using System;
 using Server.Items;
 using Server.Mobiles;
 using System.Collections.Generic;
 
 namespace Server.Engines.Quests
 {
     public class BeaconOfHarmony : BaseQuest
     {
         public BeaconOfHarmony()
             : base()
         {
             List<Type> mongbats = new List<Type>() { typeof(Mongbat) };
 
             this.AddObjective(new CalmObjective(mongbats, 5, "Test"));
 
             this.AddReward(new BaseReward(typeof(StaffOfTheMagi), 1115679)); // Recognition for mastery of spirit soothing.
         }
 
         //Creeatures remaining to be calmed
 
         /* The Beacon of Harmony */
         public override object Title
         {
             get
             {
                 return 1115677;
             }
         }
 
         /* This quest is the single quest required for a player to unlock the peacemaking mastery abilities for bards. 
          * This quest can be completed multiple times to reinstate the peacemaking mastery. 
          * To prove yourself worthy, you must first be a master of peacemaking and musicianship. 
          * You must be able to calm even the most vicious beast into tranquility. */
         public override object Description
         {
             get
             {
                 return 1115676;
             }
         }
 
         /* As you wish.  I extend to you the thanks of your people for your service thus far.  If you change your mind, my proposal still stands. */
         public override object Refuse
         {
             get
             {
                 return 1115521;
             }
         }
 
         public override void OnCompleted()
         {
 
         }
 
 
         //Objective is Clam five mongbats
         //Recognition
         public override bool CanOffer()
         {
             PlayerMobile pm = this.Owner as PlayerMobile;
 
             if (pm.Skills.Musicianship.Base < 90.0 || pm.Skills.Peacemaking.Base < 90.0)
                 return false;
             else
                 return true;
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