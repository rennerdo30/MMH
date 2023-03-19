using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Application.games.KKS
{
    internal class KKS : ModdedGame
    {

        private static readonly string MISSING_MOD_LOG_MSG_TYPE_1 = "WARNING! Missing mod detected!"; // WARNING! Missing mod detected! [[Hanmen] NextGenTex]  
        private static readonly string MISSING_MOD_LOG_MSG_TYPE_2 = "Missing zipmod! Some items are missing! -"; // Missing zipmod! Some items are missing! - [Hanmen] NextGenTex
        private static readonly string MISSING_MOD_LOG_MSG_TYPE_3 = "Missing mod detected"; // Missing mod detected [Helena bride Skirt] but matching ID found
        private static readonly string MISSING_MOD_LOG_MSG_TYPE_3_2 = "but matching ID found";

        public KKS(string installDir) : base(installDir)
        {
            this.infoPrefix = "KKS";
        }

        /*

        [Warning, Message:Sideloader] Missing zipmod! Some items are missing! - Garnetfanatico : Talim
        [Warning:Sideloader] [UAR] WARNING! Missing mod detected! [Talim]  https://https://www.patreon.com/m/garnetfanatico
        [Warning, Message:Sideloader] Missing zipmod! Some items are missing! - Garnetfanatico : Talim Costume SCVI
        [Warning:Sideloader] [UAR] WARNING! Missing mod detected! [Talim Costume SCVI]  https://www.patreon.com/m/garnetfanatico


         */


        public override List<GameMod> findMissingMods()
        {
            HashSet<GameMod> mods = new HashSet<GameMod>();

            using (StreamReader reader = new StreamReader($"{this.InstallDir}/output_log.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);

                    GameMod type1 = type1Check(line);
                    if (type1 != null)
                    {
                        mods.Add(type1);
                        continue;
                    }

                    GameMod type2 = type2Check(line);
                    if (type2 != null)
                    {
                        mods.Add(type2);
                        continue;
                    }

                    GameMod type3 = type3Check(line);
                    if (type3 != null)
                    {
                        mods.Add(type3);
                        continue;
                    }


                }
            }

            return mods.ToList();
        }

        private static GameMod type1Check(string line)
        {
            if (line.Contains(MISSING_MOD_LOG_MSG_TYPE_1))
            {
                string tmpStr = line.Substring(line.IndexOf(MISSING_MOD_LOG_MSG_TYPE_1) + MISSING_MOD_LOG_MSG_TYPE_1.Length).Trim();


                string modName = tmpStr.Substring(1, tmpStr.LastIndexOf("]") - 1);
                string url = tmpStr.Substring(tmpStr.LastIndexOf("]") + 1);

                if (modName.Trim().Length > 0)
                {
                    GameMod mod = new GameMod(modName, url, line);
                    return mod;
                }
            }

            return null;
        }

        private static GameMod type2Check(string line)
        {
            if (line.Contains(MISSING_MOD_LOG_MSG_TYPE_2))
            {
                string tmpStr = line.Substring(line.IndexOf(MISSING_MOD_LOG_MSG_TYPE_2) + MISSING_MOD_LOG_MSG_TYPE_2.Length).Trim();


                string modName = tmpStr.Trim();
                string url = "";

                if (modName.Trim().Length > 0)
                {
                    GameMod mod = new GameMod(modName, url, line);
                    return mod;
                }
            }

            return null;
        }

        private static GameMod type3Check(string line)
        {
            if (line.Contains(MISSING_MOD_LOG_MSG_TYPE_3))
            {
                string tmpStr = line.Substring(line.IndexOf(MISSING_MOD_LOG_MSG_TYPE_3) + MISSING_MOD_LOG_MSG_TYPE_3.Length).Trim();


                string modName = tmpStr.Substring(1, tmpStr.LastIndexOf("]") - 1).Replace(MISSING_MOD_LOG_MSG_TYPE_3_2, "");
                string url = "";

                if (modName.Trim().Length > 0)
                {
                    GameMod mod = new GameMod(modName, url, line);
                    return mod;
                }
            }

            return null;
        }


    }
}
