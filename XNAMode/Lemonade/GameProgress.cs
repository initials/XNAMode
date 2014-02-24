namespace Lemonade
{
    public class GameProgress
    {
        #region Fields

        public bool KilledArmy;
        public bool KilledChef;
        public bool KilledInspector;
        public bool KilledWorker;
        public bool LevelComplete;

        #endregion


        /// <summary>
        /// Constructs a new bloom settings descriptor.
        /// </summary>
        public GameProgress(bool killedArmy,
            bool killedChef,
            bool killedInspector,
            bool killedWorker, bool levelComplete)
        {
            KilledArmy = killedArmy;
            KilledChef = killedChef;
            KilledInspector = killedInspector;
            KilledWorker = killedWorker;
            LevelComplete = levelComplete;

        }
    }
}
