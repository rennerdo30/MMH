using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.games
{
    internal abstract class ModdedGame
    {
        protected readonly string installDir;


        public ModdedGame(string installDir)
        {
            this.installDir = installDir;
        }

        public abstract List<GameMod> findMissingMods();


    }
}
