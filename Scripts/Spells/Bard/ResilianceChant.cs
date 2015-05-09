using System;
 using System.Collections.Generic;
 using Server.Engines.PartySystem;
 using Server.Items;
 using Server.Mobiles;
 
 namespace Server.Spells.Bard
 {
     public class ResilianceChant : BardChant
     {
         private static readonly SpellInfo m_Info = new SpellInfo(
             "Resiliance", "Kal Mani Tym",
             -1,
             9002);
         public ResilianceChant(Mobile caster, Item scroll)
             : base(caster, scroll, m_Info)
         {
             
         }
 
         public override Type SongType { get { return typeof(ResilianceUpkeepTimer); } }
 
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
                 return 16;
             }
         }
 
         public override int UpkeepCost
         {
             get { return 4; }
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
                 new ResilianceUpkeepTimer(this.UpkeepCost, this.Caster).Start();
             this.FinishSequence();
         }
 
     }
 
     public class ResilianceUpkeepTimer : BardTimer
     {
         public ResilianceUpkeepTimer(int upkeep, Mobile caster)
             : base(caster, true, BardEffect.Resilience)
         {
 
         }
 
         protected override bool IsTarget(Mobile m)
         {
             if (this.m_Caster == m || !m.Criminal) // party members
                 return true;
 
             return false;
         }
 
         protected override void StartEffect(Mobile m)
         {
             //Sanity Check
             if (!(m is PlayerMobile)) return;
 
             PlayerMobile target = m as PlayerMobile;
             BardHelper.AddEffect(m_Caster, target, BardEffect.Resilience);
 
             base.StartEffect(m);
         }
 
         protected override void EndEffect(Mobile m)
         {
             if (!(m is PlayerMobile)) return;
 
             PlayerMobile target = m as PlayerMobile;
 
             target.BardEffects.Remove(BardEffect.Resilience);
 
             base.EndEffect(m);
         }
     }
 }