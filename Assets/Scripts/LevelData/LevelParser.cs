   /* 
    Licensed under the Apache License, Version 2.0
    http://www.apache.org/licenses/LICENSE-2.0
    */
   using UnityEngine;
   using System;
   using System.Xml;
   using System.Xml.Serialization;
   using System.Collections.Generic;
   using System.IO;

   public class LevelParser : MonoBehaviour
   {
	   private XmlDocument xmlDoc;

	   public static LevelParser instance = null;
	   
	   MapData i = new MapData();
    
	   void Awake()
	   {
		   instance = instance == null ? new LevelParser() : instance;
		   instance.InitLevelsSO();
	   }

	   public void InitLevelsSO()
	   {
		   DeserializeObject("4kvvvv");
	   }
	   

	   private void DeserializeObject(string filename)
	   {
		   Console.WriteLine("Reading with Stream");
        
		   XmlSerializer serializer = new XmlSerializer(typeof(MapData));

		   FileStream fs = new FileStream(Application.dataPath + "/Resources/vvvvvv/" + filename + ".vvvvvv", FileMode.OpenOrCreate);
		   TextReader reader = new StreamReader(fs);
		   i = (MapData) serializer.Deserialize(reader);
        
		   Debug.Log(
			   i.Data.MetaData.Creator + "\t" +
			   i.Data.EdEntities.Edentity[0].X + "\t" +
			   i.Data.EdEntities.Edentity[1].X + "\t" +
			   i.Data.LevelMetaData.EdLevelClass[0].Enemytype + "\t" + " ...");

		   Resources.Load<LevelScriptableObject>("Levels/Level_0").mapData = i;
	   }
   }
   
   [XmlRoot(ElementName="MapData"), Serializable]
   public class MapData {
	   [XmlElement(ElementName="Data")]
	   public Data Data { get; set; }
	   [XmlAttribute(AttributeName="version")]
	   public string Version { get; set; }
   }
   
	//[XmlRoot(ElementName="MetaData"), Serializable]
	[Serializable]
	public class MetaData {
		[XmlElement(ElementName="Creator")]
		public string Creator { get; set; }
		[XmlElement(ElementName="Title")]
		public string Title { get; set; }
		[XmlElement(ElementName="Created")]
		public string Created { get; set; }
		[XmlElement(ElementName="Modified")]
		public string Modified { get; set; }
		[XmlElement(ElementName="Modifiers")]
		public string Modifiers { get; set; }
		[XmlElement(ElementName="Desc1")]
		public string Desc1 { get; set; }
		[XmlElement(ElementName="Desc2")]
		public string Desc2 { get; set; }
		[XmlElement(ElementName="Desc3")]
		public string Desc3 { get; set; }
		[XmlElement(ElementName="website")]
		public string Website { get; set; }
	}

   //[XmlRoot(ElementName="edentity"), Serializable]
   [Serializable]
   public class Edentity {
		[XmlAttribute(AttributeName="x")]
		public string X { get; set; }
		[XmlAttribute(AttributeName="y")]
		public string Y { get; set; }
		[XmlAttribute(AttributeName="t")]
		public string T { get; set; }
		[XmlAttribute(AttributeName="p1")]
		public string P1 { get; set; }
		[XmlAttribute(AttributeName="p2")]
		public string P2 { get; set; }
		[XmlAttribute(AttributeName="p3")]
		public string P3 { get; set; }
		[XmlAttribute(AttributeName="p4")]
		public string P4 { get; set; }
		[XmlAttribute(AttributeName="p5")]
		public string P5 { get; set; }
		[XmlAttribute(AttributeName="p6")]
		public string P6 { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

   //[XmlRoot(ElementName="edEntities"), Serializable]
   [Serializable]
   public class EdEntities {
		[XmlElement(ElementName="edentity")]
		public List<Edentity> Edentity { get; set; }
	}


   //[XmlRoot(ElementName="edLevelClass"), Serializable]
   [Serializable]
   public class EdLevelClass {
		[XmlAttribute(AttributeName="tileset")]
		public string Tileset { get; set; }
		[XmlAttribute(AttributeName="tilecol")]
		public string Tilecol { get; set; }
		[XmlAttribute(AttributeName="platx1")]
		public string Platx1 { get; set; }
		[XmlAttribute(AttributeName="platy1")]
		public string Platy1 { get; set; }
		[XmlAttribute(AttributeName="platx2")]
		public string Platx2 { get; set; }
		[XmlAttribute(AttributeName="platy2")]
		public string Platy2 { get; set; }
		[XmlAttribute(AttributeName="platv")]
		public string Platv { get; set; }
		[XmlAttribute(AttributeName="enemyx1")]
		public string Enemyx1 { get; set; }
		[XmlAttribute(AttributeName="enemyy1")]
		public string Enemyy1 { get; set; }
		[XmlAttribute(AttributeName="enemyx2")]
		public string Enemyx2 { get; set; }
		[XmlAttribute(AttributeName="enemyy2")]
		public string Enemyy2 { get; set; }
		[XmlAttribute(AttributeName="enemytype")]
		public string Enemytype { get; set; }
		[XmlAttribute(AttributeName="warpdir")]
		public string Warpdir { get; set; }
	}

   //[XmlRoot(ElementName="levelMetaData"), Serializable]
   [Serializable]
   public class LevelMetaData {
		[XmlElement(ElementName="edLevelClass")]
		public List<EdLevelClass> EdLevelClass { get; set; }
	}

   //[XmlRoot(ElementName="Data"), Serializable]
   [Serializable]
   public class Data {
		[XmlElement(ElementName="MetaData")]
		public MetaData MetaData { get; set; }
		[XmlElement(ElementName="mapwidth")]
		public string Mapwidth { get; set; }
		[XmlElement(ElementName="mapheight")]
		public string Mapheight { get; set; }
		[XmlElement(ElementName="levmusic")]
		public string Levmusic { get; set; }
		[XmlElement(ElementName="contents")]
		public string Contents { get; set; }
		[XmlElement(ElementName="edEntities")]
		public EdEntities EdEntities { get; set; }
		[XmlElement(ElementName="levelMetaData")]
		public LevelMetaData LevelMetaData { get; set; }
		[XmlElement(ElementName="script")]
		public string Script { get; set; }
	}

	