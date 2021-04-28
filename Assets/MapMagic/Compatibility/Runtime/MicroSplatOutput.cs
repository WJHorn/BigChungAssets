#if MAPMAGIC2 //shouldn't work if MM assembly not compiled

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Den.Tools;
using Den.Tools.Matrices;
using MapMagic.Products;

namespace MapMagic.Nodes.MatrixGenerators {
	[System.Serializable]
	[GeneratorMenu(
		menu = "Map/Output", 
		name = "MicroSplat", 
		section =2,
		drawButtons = false,
		colorType = typeof(MatrixWorld), 
		iconName="GeneratorIcons/TexturesOut",
		helpLink = "https://gitlab.com/denispahunov/mapmagic/wikis/output_generators/Textures")]
	public class MicroSplatOutput200 : BaseTexturesOutput<MicroSplatOutput200.MicroSplatLayer>
	{
        //public static Material material;  //in globals
		//public static MicroSplatPropData propData;
		//public static bool assignComponent;

        public class MicroSplatLayer : BaseTextureLayer 
		{ 
			[NonSerialized] public TerrainLayer prototype = null; //used in case 'add std' enabled
		}


		public override void Generate (TileData data, StopToken stop) 
		{
			//generating
			MatrixWorld[] dstMatrices = BaseGenerate(data, stop);

			//adding to finalize
			if (stop!=null && stop.stop) return;
			for (int i=0; i<layers.Length; i++)
				data.StoreOutput(layers[i], typeof(MicroSplatOutput200), layers[i],  dstMatrices[i]);
			data.MarkFinalize(Finalize, stop);
		}

		public override FinalizeAction FinalizeAction => finalizeAction; //should return variable, not create new
		public static FinalizeAction finalizeAction = Finalize; //class identified for FinalizeData
		public static void Finalize (TileData data, StopToken stop) 
		{
            #if __MICROSPLAT__

			//creating control textures contents
			if (stop!=null && stop.stop) return;
			data.GatherOutputs (typeof(MicroSplatOutput200),
				out MicroSplatLayer[] layers,
				out MatrixWorld[] matrices,
				out MatrixWorld[] masks,
				inSubs:true);


			//purging if no outputs
			if (matrices.Length == 0)
			{
				if (stop!=null && stop.stop) return;
				data.MarkApply(CustomShaderOutput200.ApplyData.Empty);
				return;
			}

			IApplyData applyData = null;

			//custom splatmaps
			if (data.globals.microSplatApplyType==Core.Globals.MicroSplatApplyType.Textures  ||  
				data.globals.microSplatApplyType==Core.Globals.MicroSplatApplyType.Both)
			{
				float[] opacities = layers.Select(l=>l.Opacity);
				int[] channelNums = layers.Select(l=>l.channelNum);

				if (stop!=null && stop.stop) return;
				Color[][] colors = CustomShaderOutput200.BlendMatrices(data.area.active.rect, matrices, masks, opacities, channelNums);
				string[] names = data.globals.useCustomControlTextures ?
					new string[] {"_CustomControl0", "_CustomControl1", "_CustomControl2", "_CustomControl3"} :
					new string[] {"_Control0", "_Control1", "_Control2", "_Control3"};

				//custom normal map
				if (data.globals.microSplatNormals)
				{
					(Matrix r, Matrix g, Matrix b) = MatrixOps.NormalsSet(data.heights, data.area.PixelSize.x, data.globals.height);
					Color[] normColors = CustomShaderOutput200.MatricesToColors(data.area.active.rect, r, g, b, null);

					ArrayTools.Add(ref colors, normColors);
					ArrayTools.Add(ref names, "_PerPixelNormal");
				}

				if (stop!=null && stop.stop) return;
				applyData = new ApplyCustomData()
				{
					textureColors = colors,
					textureNames = names,
					textureFormat = TextureFormat.RGBA32,
					assignComponent = data.globals.assignComponent,
					propData = data.globals.microSplatPropData as MicroSplatPropData,
				};
			}

			//standard splatmaps
			if (data.globals.microSplatApplyType==Core.Globals.MicroSplatApplyType.Splats  ||  
				data.globals.microSplatApplyType==Core.Globals.MicroSplatApplyType.Both)
			{
				TerrainLayer[] tlayers = layers.Select(l=>l.prototype);

				if (stop!=null && stop.stop) return;
				Array.Reverse(tlayers); //Textures output (who applies) uses inversed layers order, but MS - does not
				Array.Reverse(matrices);
				Array.Reverse(masks);

				if (stop!=null && stop.stop) return;
				float[,,] splats3D = TexturesOutput200.BlendLayers(matrices, masks, data.area, stop);

				applyData = new ApplySplatsData() {
					splats = splats3D, 
					prototypes = tlayers, 
					assignComponent = data.globals.assignComponent,
					propData = data.globals.microSplatPropData as MicroSplatPropData};
			}

			Graph.OnOutputFinalized?.Invoke(typeof(CustomShaderOutput200), data, applyData, stop);
			data.MarkApply(applyData);

            #endif
		}



		public override void Purge (TileData data, Terrain terrain)
		{

		}


		public class ApplySplatsData : TexturesOutput200.ApplyData
		{
			#if __MICROSPLAT__

			public bool assignComponent;
			public MicroSplatPropData propData;

			public override void Apply (Terrain terrain)
			{
				//checking microsplat component
				//this should be done before applying control since
				//microsplat removes template from terrain on disable (lod switch), so ensuring we have a material before base.Apply
				MicroSplatTerrain mso = null;
				if (assignComponent)
				{
					mso = CheckAssignMicroSplat(terrain);
					mso.propData = propData;
				}
				else if (terrain.materialTemplate == null) //prevents an error (materialTemplate is null) on disabling "Set Component"
				{
					MapMagic.Core.MapMagicObject mapMagic = terrain.transform.parent.parent.GetComponent<MapMagic.Core.MapMagicObject>();
					terrain.materialTemplate = mapMagic.terrainSettings.material;
				}

				base.Apply(terrain);

				if (assignComponent)
					mso.Sync();
			}

			#endif
		}


		public class ApplyCustomData : CustomShaderOutput200.ApplyData
		{
			#if __MICROSPLAT__

			public bool assignComponent;
			public MicroSplatPropData propData;
			public Material materialTemplate; //source material assigned. Can't use terrain.materialTemplate since it will be changed with copy

			public override void Apply (Terrain terrain)
			{
				//checking microsplat component
				//this should be done before applying control since
				//microsplat removes template from terrain on disable (lod switch), so ensuring we have a material before base.Apply
				MicroSplatTerrain mso = null;
				if (assignComponent)
				{
					mso = CheckAssignMicroSplat(terrain);
					mso.propData = propData;
				}
				else if (terrain.materialTemplate == null) //prevents an error (materialTemplate is null) on disabling "Set Component"
				{
					MapMagic.Core.MapMagicObject mapMagic = terrain.transform.parent.parent.GetComponent<MapMagic.Core.MapMagicObject>();
					terrain.materialTemplate = mapMagic.terrainSettings.material;
				}

				base.Apply(terrain);

				if (assignComponent)
					mso.Sync(); //this will create basemap and probably other useful stuff
			}

			#endif
		}


		#if __MICROSPLAT__
		public static MicroSplatTerrain CheckAssignMicroSplat (Terrain terrain)
		{
			
			MicroSplatTerrain mso = terrain.GetComponent<MicroSplatTerrain>();
			if (mso == null) mso = terrain.gameObject.AddComponent<MicroSplatTerrain>();
			mso.terrain = terrain; //otherwise nullref on newly created tiles

			MapMagic.Core.MapMagicObject mapMagic = terrain.transform.parent.parent.GetComponent<MapMagic.Core.MapMagicObject>();
			mso.templateMaterial = mapMagic.terrainSettings.material;
			if (terrain.materialTemplate == mso.templateMaterial || terrain.materialTemplate==null) //if material instance assigned (first run)
			{
				mso.matInstance = new Material(mapMagic.terrainSettings.material);
				terrain.materialTemplate = mso.matInstance;
			}
			else
				mso.matInstance = terrain.materialTemplate;

			return mso;
		}
		#endif


		public class TmpApplyData// : IApplyData
		{
            #if __MICROSPLAT__

			public Color[][] colors; // TODO: use raw texture bytes

			public void Read (Terrain terrain) { throw new System.NotImplementedException(); }

			public void ApplyTmp (Terrain terrain)
			{
				//checking microsplat component
				MicroSplatTerrain mso = terrain.GetComponent<MicroSplatTerrain>();
				if (mso == null) mso = terrain.gameObject.AddComponent<MicroSplatTerrain>();
				mso.terrain = terrain; //otherwise nullref on newly created tiles
				mso.templateMaterial = terrain.materialTemplate;
				

				int numTextures = colors.Length;
				if (numTextures==0) return;
				int resolution = (int)Mathf.Sqrt(colors[0].Length);

				for (int t=0; t<numTextures; t++)
				{
					if (colors[t] == null) continue;

					Texture2D tex = GetTex(mso, t);
					if (tex==null || tex.width!=resolution || tex.height!=resolution || tex.format!=TextureFormat.RGBA32)
					{
						if (tex!=null)
						{
							#if UNITY_EDITOR
							if (!UnityEditor.AssetDatabase.Contains(tex))
							#endif
								GameObject.DestroyImmediate(tex);
						}

						tex = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false, true);
						tex.wrapMode = TextureWrapMode.Mirror; //to avoid border seams
						tex.name = "CustomControl " + t;
						SetTex(mso, t, tex);
						//tex.hideFlags = HideFlags.DontSave;
					}

					tex.SetPixels(0,0, tex.width,tex.height, colors[t]);
					tex.Apply();
				}

				mso.Sync();
				//terrain.basemapDistance = 1000000;	
			}

			public Texture2D GetTex (MicroSplatTerrain mso, int num)
			{
				switch (num)
				{
					case 0: return mso.customControl0;
					case 1: return mso.customControl1;
					case 2: return mso.customControl2;
					case 3: return mso.customControl3;
					case 4: return mso.customControl4;
					case 5: return mso.customControl5;
					case 6: return mso.customControl6;
					case 7: return mso.customControl7;
					default: return null;
				}
			}

			public void SetTex (MicroSplatTerrain mso, int num, Texture2D tex)
			{
				switch (num)
				{
					case 0: mso.customControl0 = tex; break;
					case 1: mso.customControl1 = tex; break;
					case 2: mso.customControl2 = tex; break;
					case 3: mso.customControl3 = tex; break;
					case 4: mso.customControl4 = tex; break;
					case 5: mso.customControl5 = tex; break;
					case 6: mso.customControl6 = tex; break;
					case 7: mso.customControl7 = tex; break;
				}
			}

			public static TmpApplyData Empty {get{ return new TmpApplyData() { colors = new Color[0][] }; }}

			public int Resolution
			{get{
				if (colors.Length==0) return 0;
				else return (int)Mathf.Sqrt(colors[0].Length);
			}}

            #endif
		}
	}
}

#endif //MAPMAGIC2