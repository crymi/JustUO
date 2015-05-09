using System;
 using System.Collections.Generic;
 using Server.Engines.PartySystem;
 using Server.Engines.Quests.Ninja;
 using Server.Items;
 using Server.Mobiles;
 using Server.Targeting;
 
 namespace Server.Spells.Bard
 {
     public class InvigorateChant : BardChant
     {
         private static readonly SpellInfo m_Info = new SpellInfo(
             "Invigorate", "An Zu",
             -1,
             9002);
         public InvigorateChant(Mobile caster, Item scroll)
             : base(caster, scroll, m_Info)
         {
             
         }
 
         public override Type SongType { get { return typeof(InvigorateUpkeepTimer); } }
 
         public override TimeSpan CastDelayBase
         {
             get
             {
                 return TimeSpan.FromSeconds(1.75);
             }
         }
 
         public override int RequiredMana
         {
             get
             {
                 return 22;
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
                 new InvigorateUpkeepTimer(this.UpkeepCost, this.Caster).Start();
             this.FinishSequence();
         }
 
     }
 
     public class InvigorateUpkeepTimer : BardTimer
     {
 
         private Dictionary<Mobile, DateTime> m_LastHeal = new Dictionary<Mobile, DateTime>(); 
 
         public InvigorateUpkeepTimer(int upkeep, Mobile caster)
             : base(caster, true, BardEffect.Invigorate)
         {
         }
 
         protected override bool IsTarget(Mobile m)
         {
             if (m_Caster == m || !m.Criminal) // Target self or party members
                 return true;
 
             return false;
         }
 
         protected override void StartEffect(Mobile m)
         {
             //Sanity Check
             if (!(m is PlayerMobile)) return;
 
             PlayerMobile target = m as PlayerMobile;
 
             BardHelper.AddEffect(m_Caster, target, BardEffect.Invigorate);
 
             target.AddStatMod(new StatMod(StatType.All, "Invigorate", BardHelper.Scaler(m_Caster, 8, 8, 1), TimeSpan.FromHours(1.0)));
 
             target.SendLocalizedMessage(1115737); // You feel invigorated by the bard's spellsong.
 
             base.StartEffect(m);
         }
 
         protected override void Effect(Mobile m)
         {
             if (!m_LastHeal.ContainsKey(m) || m_LastHeal[m] <= DateTime.UtcNow)
             {
                 m.Heal(BardHelper.Scaler(m_Caster, 4, 16, 1), m_Caster);
                 m_LastHeal[m] = DateTime.UtcNow;
             }
 
             base.Effect(m);
         }
 
         protected override void EndEffect(Mobile m)
         {
             if (!(m is PlayerMobile)) return;
 
             PlayerMobile target = m as PlayerMobile;
 
             target.BardEffects.Remove(BardEffect.Invigorate);
             target.RemoveStatMod("Invigorate");
 
             base.EndEffect(m);
         }
     }
 }