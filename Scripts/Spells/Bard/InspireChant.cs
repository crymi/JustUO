 #region References
 
 using System;
 using Server.Mobiles;
 
 #endregion
 
 namespace Server.Spells.Bard
 {
     public class InspireChant : BardChant
     {
         private static readonly SpellInfo m_Info = new SpellInfo(
             "Inspire", "Uus Por",
             -1,
             9002);
 
         public InspireChant(Mobile caster, Item scroll)
             : base(caster, scroll, m_Info)
         {
         }
 
         public override Type SongType { get { return typeof (InspireUpkeepTimer); } }
 
         public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(1.75); } }
 
         public override int RequiredMana { get { return 16; } }
 
         public override int UpkeepCost { get { return 4; } }
 
         public override bool BlocksMovement { get { return false; } }
 
         public override void OnCast()
         {
             if (CanSing())
                 new InspireUpkeepTimer(Caster).Start();
 
             FinishSequence();
         }
     }
 
     public class InspireUpkeepTimer : BardTimer
     {
         public InspireUpkeepTimer( Mobile caster)
             : base(caster, true, BardEffect.Inspire)
         {
         }
 
         protected override bool IsTarget(Mobile m)
         {
             if (m_Caster == m || !m.Criminal)
                 return true;
 
             return false;
         }
 
         protected override void StartEffect(Mobile m)
         {
             PlayerMobile target = m as PlayerMobile;
             //Sanity Check
             if (target == null) return;
 
 
             BardHelper.AddEffect(m_Caster, target, BardEffect.Inspire);
 
             base.StartEffect(m);
         }
 
         protected override void EndEffect(Mobile m)
         {
             if (!(m is PlayerMobile)) return;
 
             PlayerMobile target = m as PlayerMobile;
 
             target.BardEffects.Remove(BardEffect.Inspire);
 
             base.EndEffect(m);
         }
     }
 }