using System;

namespace Server.Items
{
    public class BookOfBardMasteries : Spellbook
    {
        [Constructable]
        public BookOfBardMasteries()
            : this((ulong)0x3F)
        {
        }

        [Constructable]
        public BookOfBardMasteries(ulong content)
            : base(content, 0x225a)
        {
            this.Layer = (Core.ML ? Layer.OneHanded : Layer.Invalid);
        }

        public BookOfBardMasteries(Serial serial)
            : base(serial)
        {
        }

        public override SpellbookType SpellbookType
        {
            get
            {
                return SpellbookType.Bard;
            }
        }

        public override int BookOffset
        {
            get
            {
                return 700;
            }
        }
        public override int BookCount
        {
            get
            {
                return 6;
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0 && Core.ML)
                this.Layer = Layer.OneHanded;
        }
    }
}