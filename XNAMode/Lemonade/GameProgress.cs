namespace Lemonade
{
    public class GameProgress
    {
        #region Fields

        public bool KilledArmy;
        public bool KilledChef;
        public bool KilledInspector;
        public bool KilledWorker;

        #endregion


        /// <summary>
        /// Constructs a new bloom settings descriptor.
        /// </summary>
        public GameProgress(bool killedArmy,
            bool killedChef,
            bool killedInspector,
            bool killedWorker)
        {
            KilledArmy = killedArmy;
            KilledChef = killedChef;
            KilledInspector = killedInspector;
            KilledWorker = killedWorker;

        }
    }
}
