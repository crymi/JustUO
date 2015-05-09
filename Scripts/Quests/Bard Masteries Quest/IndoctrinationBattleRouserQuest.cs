using System;
 using Server.Items;
 using Server.Mobiles;
 using System.Collections.Generic;
 
 namespace Server.Engines.Quests
 {
     public class IndoctrinationBattleRouserQuest : BaseQuest
     {
         public IndoctrinationBattleRouserQuest()
             : base()
         {
             List<Type> rabbits = new List<Type>() { typeof(JackRabbit), typeof(Rabbit) };
             List<Type> healers = new List<Type>() { typeof(WanderingHealer) };
 
             this.AddObjective(new InciteObjective(rabbits, healers, 5, "Test"));
 
             this.AddReward(new BaseReward(typeof(StaffOfTheMagi), 1115659));
         }
 
         /* Indoctrination of a Battle Rouser */
         public override object Title
         {
             get
             {
                 return 1115657;
             }
         }
 
         /* This quest is the single quest required for a player to unlock the provocation 
         mastery abilities for bards. This quest can be completed multiple times 
         to reinstate the provocation mastery. To prove yourself worthy, you must be 
         able to incite even the most peaceful to frenzied battle lust. */
         public override object Description
         {
             get
             {
                 return 1115656;
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
 
         public override bool CanOffer()
         {
             PlayerMobile pm = this.Owner as PlayerMobile;
 
             if (pm.Skills.Musicianship.Base < 90.0 || pm.Skills.Provocation.Base < 90.0)
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