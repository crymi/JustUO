using System;
 using System.Collections.Generic;
 using Server.Engines.PartySystem;
 using Server.Engines.Quests.Ninja;
 using Server.Items;
 using Server.Mobiles;
 using Server.Targeting;
 
 namespace Server.Spells.Bard
 {
     public class PerseveranceChant : BardChant
     {
         private static readonly SpellInfo m_Info = new SpellInfo(
             "Perseverance", "Uus Jux Sanct",
             -1,
             9002);
         public PerseveranceChant(Mobile caster, Item scroll)
             : base(caster, scroll, m_Info)
         {
             
         }
 
         public override Type SongType { get { return typeof (PerseveranceUpkeepTimer); } }
 
         public override TimeSpan CastDelayBase
         {
             get
             {
                 return TimeSpan.FromSeconds(1.75);
             }
         }
         public override double RequiredSkill
         {
             get
             {
                 return 90.0;
             }
         }
         public override int RequiredMana
         {
             get
             {
                 return 18;
             }
         }
 
         public override int UpkeepCost
         {
             get { return 5; }
         }
 
         public override bool BlocksMovement
         {
             get
             {
                 return false;
             }
         }
         public override bool DelayedDamage
         {
             get
             {
                 return false;
             }
         }
         public override void OnCast()
         {
             if (CanSing())
                 new PerseveranceUpkeepTimer(this.UpkeepCost, this.Caster).Start();
             this.FinishSequence();
         }
 
     }
 
     public class PerseveranceUpkeepTimer : BardTimer
     {
 
         private int m_Heal;
 
         public PerseveranceUpkeepTimer(int upkeep, Mobile caster)
             : base(caster, true, BardEffect.Perseverance)
         {
             m_Heal = 0;
         }
 
         protected override bool IsTarget(Mobile m)
         {
             if (m_Caster == m || !m.Criminal)
                 return true;
 
             return false;
         }
 
         protected override void StartEffect(Mobile m)
         {
             //Sanity Check
             if (!(m is PlayerMobile)) return;
 
             PlayerMobile target = m as PlayerMobile;
 
             BardHelper.AddEffect(m_Caster, target, BardEffect.Perseverance);
 
             base.StartEffect(m);
         }
 
         protected override void EndEffect(Mobile m)
         {
             if (!(m is PlayerMobile)) return;
 
             PlayerMobile target = m as PlayerMobile;
 
             target.BardEffects.Remove(BardEffect.Perseverance);
 
             base.EndEffect(m);
         }
     }
 }