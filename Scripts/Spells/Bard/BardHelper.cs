 #region References
 
 using System;
 using System.Linq;
 using Server.Mobiles;
 
 #endregion
 
 namespace Server.Spells.Bard
 {
     public enum BardEffect
     {
         Inspire = 0,
         Invigorate,
         Resilience,
         Perseverance,
         Tribulation,
         Despair
     }
 
     public static class BardHelper
     {
         public static BuffInfo GenerateBuffInfo(BardEffect effect, Mobile caster)
         {
             switch (effect)
             {
                 case BardEffect.Inspire:
                     int HCI_SPI = Scaler(caster, 4, 16, 1);
                     int DI = Scaler(caster, 20, 40, 3);
                     int BDM = Scaler(caster, 1, 15, 0);
                     return new BuffInfo(BuffIcon.Inspire, 1115612, 1151951,
                         String.Format("{0}\t{1}\t{2}\t{3}", HCI_SPI, HCI_SPI, DI, BDM), false);
                     // Inspire ~1_HCI~% Hit Chance Increase.<br>~2_SDI~% Spell Damage Increase.<br>~3_DI~% Damage Increase.<br>Bonus Damage Modifier:  ~4_DM~%
 
                 case BardEffect.Invigorate:
                     int statIncrease = Scaler(caster, 8, 8, 2);
                     int HPI = Scaler(caster, 20, 20, 2);
                     return new BuffInfo(BuffIcon.Invigorate, 1115613, 1115730,
                         String.Format("{0}\t{1}\t{2}\t{3}", HPI, statIncrease, statIncrease, statIncrease), false);
                     // Invigorate ~1_HPI~ Hit Point Increase.<br>~2_STR~ Strength.<br>~3_INT~ Intelligence.<br>~4_DEX~ Dexterity.<br>
 
                 case BardEffect.Resilience:
                     int regenBonus = Scaler(caster, 2, 16, 2);
                     return new BuffInfo(BuffIcon.Resilience, 1115614, 1115731,
                         String.Format("{0}\t{1}\t{2}", regenBonus, regenBonus, regenBonus), false);
                     // Resilience ~1_HPR~ Hit Point Regeneration.<br>~2_SR~ Stamina Regeneration.<br>~3_MR~ Mana Regeneration.<br>Curse Durations Reduced.<br>Resistance to Poison.<br>Bleed Duration Reduced.<br>Mortal Wound Duration Reduced.
 
                 case BardEffect.Perseverance:
                     int DCI_DTR = Scaler(caster, 2, 24, 1);
                     int castingFocus = Scaler(caster, 1, 4, 0.33);
                     return new BuffInfo(BuffIcon.Perseverance, 1115615, 1115732,
                         String.Format("{0}\t{1}\t{2}", DCI_DTR, DCI_DTR, castingFocus), false);
                     // Perseverance ~1_DCI~% Defense Chance Increase.<br>~2_DAM~% Damage Taken.<br>~3_CF~% Casting Focus.<br>
 
                 case BardEffect.Tribulation:
                     int HCID_SPID = Scaler(caster, -5, -22, -1.66);
                     int damageBurst = Scaler(caster, 15, 60, 4);
                     return new BuffInfo(BuffIcon.Tribulation, 1115616, 1115742,
                         String.Format("{0}\t{1}\t{2}", HCID_SPID, HCID_SPID, damageBurst), false);
                     // Tribulation ~1_HCI~% Hit Chance.<br>~2_SDI~% Spell Damage.<br>Damage taken has a ~3_EXP~% chance to cause additional burst of physical damage.<br>
 
                 case BardEffect.Despair:
                     int strDecrease = Scaler(caster, -4, -16, -1);
                     int dotDmg = Scaler(caster, 9, 36, 6);
                     return new BuffInfo(BuffIcon.Despair, 1115617, 1115743,
                         String.Format("{0}\t{1}", strDecrease, dotDmg), false);
                     // Despair ~1_STR~ Strength.<br>~2_DAM~ physical damage every 2 seconds while spellsong remains in effect.<br>
             }
 
             return new BuffInfo(BuffIcon.Bless, 500000);
         }
 
         public static BuffInfo[] BardBuffInfo = new BuffInfo[6]
         {
             new BuffInfo(BuffIcon.MagicReflection, 1115612, false), // Inspire
             new BuffInfo(BuffIcon.MagicReflection, 1115613, false),
             new BuffInfo(BuffIcon.MagicReflection, 1115614, false), // Resilience
             new BuffInfo(BuffIcon.MagicReflection, 1115615, false), // Perseverance
             new BuffInfo(BuffIcon.MagicReflection, 1115616, false), // Tribulation
             new BuffInfo(BuffIcon.MagicReflection, 1115617, false) // Despair
         };
 
         public static int[] BardUpkeepCosts = new int[6]
         {
             4, // Inspire
             5, // Invigorate
             4, // Resilience
             5, // Perseverance
             8, // Tribulation
             10 // Despair
         };
 
         public static int GetUpkeepCost(Mobile caster, BardEffect effect, int targets = 0)
         {
             int cost = BardUpkeepCosts[(int) effect];
 
             cost = targets/5;
 
             if (caster.Skills.Peacemaking.Base > 100.0)
                 cost -= 1;
             if (caster.Skills.Discordance.Base > 100.0)
                 cost -= 1;
             if (caster.Skills.Provocation.Base > 100.0)
                 cost -= 1;
 
             return cost;
         }
 
         public static int Scaler(Mobile mobile, int low, int high, double complementryModifier, bool complementrySkillsOnly = true)
         {
             int complementryPoints = 0;
 
             if (mobile.Skills.Peacemaking.Base > 100)
                 complementryPoints = (int) Math.Floor((mobile.Skills.Peacemaking.Base - 100.0)/10);
 
             if (mobile.Skills.Discordance.Base > 100)
                 complementryPoints = (int) Math.Floor((mobile.Skills.Discordance.Base - 100.0)/10);
 
             if (mobile.Skills.Provocation.Base > 100)
                 complementryPoints = (int) Math.Floor((mobile.Skills.Provocation.Base - 100.0)/10);
 
             if (!complementrySkillsOnly && mobile.Skills.Musicianship.Base > 100)
                 complementryPoints = (int) Math.Floor((mobile.Skills.Musicianship.Base - 100.0)/10);
 
 
             double scale = (high - low)/(120.0 - 90.0);
 
             double value = (  ((mobile.Skills.Musicianship.Base - 90) * scale));
 
             value = (complementryModifier * complementryPoints);
 
             return (int)value;
         }
 
         public static BardTimer GetActiveSong(Mobile mobile, Type songType)
         {
             if (!(mobile is PlayerMobile)) return null;
 
             return ((PlayerMobile) mobile).ActiveSongs.FirstOrDefault(song => song.GetType() == songType);
         }
 
         public static void AddEffect(Mobile caster, Mobile target, BardEffect effect)
         {
             if (target is PlayerMobile && !((PlayerMobile)target).BardEffects.ContainsKey(effect))
                 ((PlayerMobile) target).BardEffects.Add(effect, caster);
             else if (target is BaseCreature && !((BaseCreature)target).BardEffects.ContainsKey(effect))
                 ((BaseCreature) target).BardEffects.Add(effect, caster);
         }
 
         public static void RemoveEffect(Mobile target, BardEffect effect)
         {
             if (target is PlayerMobile && ((PlayerMobile)target).BardEffects.ContainsKey(effect))
                 ((PlayerMobile)target).BardEffects.Remove(effect);
             else if (target is BaseCreature && ((BaseCreature)target).BardEffects.ContainsKey(effect))
                 ((BaseCreature)target).BardEffects.Remove(effect);
         }
 
         public static Mobile HasEffect(Mobile target, BardEffect effect)
         {
             if (target is PlayerMobile)
                 return ((PlayerMobile)target).BardEffects.ContainsKey(effect) ? ((PlayerMobile) target).BardEffects[effect] : null;
             
             if (target is BaseCreature)
                 return ((BaseCreature)target).BardEffects.ContainsKey(effect) ? ((BaseCreature)target).BardEffects[effect] : null;
 
             return null;
         }
     }
 }