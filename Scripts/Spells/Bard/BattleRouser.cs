using System.Collections.Generic;
 using System;
 using Server.Items;
 
 namespace Server.Engines.Quests
 {
     public class BattleRouser : MondainQuester
     {
         [Constructable]
         public BattleRouser(string name)
             : base(name, "the Battle Rouser")
         {
         }
 
         public BattleRouser(Serial serial)
             : base(serial)
         {
         }
 
         public override Type[] Quests
         {
             get
             {
                 return new Type[] 
                 {
                     typeof(IndoctrinationBattleRouserQuest)
                 };
             }
         }
         public override void InitBody()
         {
             this.InitStats(100, 100, 25);
 
             this.Female = false;
             this.Race = Race.Human;
 
             this.Hue = 0x840C;
             this.HairItemID = 0x2045;
             this.HairHue = 0x453;
         }
 
         public override void InitOutfit()
         {
             this.AddItem(new Backpack());
             this.AddItem(new Sandals(0x74A));
             this.AddItem(new Robe(0x498));
         }
 
         public override void Serialize(GenericWriter writer)
         {
             base.Serialize(writer);
 
             writer.Write((int)0); // version
         }
 
         public override void Deserialize(GenericReader reader)
         {
             base.Deserialize(reader);
 
             int version = reader.ReadInt();
         }
     }
 
     public class SirHareus : BattleRouser
     {
         [Constructable]
         public SirHareus()
             : base("Sir Hareus")
         {
 
         }
 
         public SirHareus(Serial serial)
             : base(serial)
         {
              
         }
 
         public override void Serialize(GenericWriter writer)
         {
             base.Serialize(writer);
 
             writer.Write((int)0); // version
         }
 
         public override void Deserialize(GenericReader reader)
         {
             base.Deserialize(reader);
 
             int version = reader.ReadInt();
         }
     }
 }