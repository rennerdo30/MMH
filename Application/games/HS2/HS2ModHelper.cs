using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.games.HS2
{
    internal class HS2 : ModdedGame
    {

        private static readonly string MISSING_MOD_LOG_MSG_TYPE_1 = "WARNING! Missing mod detected!"; // WARNING! Missing mod detected! [[Hanmen] NextGenTex]  
        private static readonly string MISSING_MOD_LOG_MSG_TYPE_2 = "Missing zipmod! Some items are missing!"; // Missing zipmod! Some items are missing! - [Hanmen] NextGenTex
        private static readonly string MISSING_MOD_LOG_MSG_TYPE_3 = "Missing mod detected"; // Missing mod detected [Helena bride Skirt] but matching ID found

        /*

        [Warning, Message:Sideloader] Missing zipmod! Some items are missing! - Garnetfanatico : Talim
        [Warning:Sideloader] [UAR] WARNING! Missing mod detected! [Talim]  https://https://www.patreon.com/m/garnetfanatico
        [Warning, Message:Sideloader] Missing zipmod! Some items are missing! - Garnetfanatico : Talim Costume SCVI
        [Warning:Sideloader] [UAR] WARNING! Missing mod detected! [Talim Costume SCVI]  https://www.patreon.com/m/garnetfanatico


         */


        public List<GameMod> findMissingMods()
        {

            using (StreamReader reader = new StreamReader($"{this.installDir}/output_log.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);

                    type1Check(line);

                    if (line.Contains(msg))
                    {
                        string modName = util.StringUtil.getBetween(line, msg,);

                        Console.WriteLine("missing mod!");
                    }
                }
            }
        }

        private static GameMod type1Check(string line)
        {

        }
    }
}
