using System;
 using Server.Items;
 using Server.Mobiles;
 using System.Collections.Generic;
 
 namespace Server.Engines.Quests
 {
     public class WieldingSonicBlade : BaseQuest
     {
         public WieldingSonicBlade()
             : base()
         {
             List<Type> goats = new List<Type>() { typeof(Goat) };
 
             this.AddObjective(new DiscordObjective(goats, 5, "Test"));
 
             this.AddReward(new BaseReward(typeof(StaffOfTheMagi), 1115699)); // Recognition for mastery of song wielding.
         }
 
 
         /* Wielding the Sonic Blade */
         public override object Title
         {
             get
             {
                 return 1115696;
             }
         }
 
         /* This quest is the single quest required for a player to unlock the discordance mastery abilities for bards. 
          * This quest can be completed multiple times to reinstate the discordance mastery. 
          * To prove yourself worthy, you must first be a master of discordance and musicianship. 
          * You must be willing to distort your notes to bring pain to even the most indifferent ears. */
         public override object Description
         {
             get
             {
                 return 1115697;
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
 
             if (pm.Skills.Musicianship.Base < 90.0 || pm.Skills.Discordance.Base < 90.0)
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