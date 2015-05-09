using System;
using Server.Commands;

namespace Server
{
    public static class HighSeas
    { 
        public static void Initialize()
        {
            CommandSystem.Register("DecorateHS", AccessLevel.Administrator, new CommandEventHandler(DecorateHS_OnCommand));
        }

        [Usage("DecorateHS")]
        [Description("Generates High Seas world decoration.")]
        private static void DecorateHS_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Generating High Seas world decoration, please wait.");
			
            //Decorate.Generate("Data/Decoration/Stygian Abyss/Ter Mur", Map.TerMur);
            Decorate.Generate("Data/Decoration/High Seas", Map.Trammel, Map.Felucca);
            //Decorate.Generate("Data/Decoration/Stygian Abyss/Felucca", Map.Felucca);

            e.Mobile.SendMessage("High Seas world generation complete.");
        }
    }
}