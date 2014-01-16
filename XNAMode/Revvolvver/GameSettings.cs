#region File Description
//-----------------------------------------------------------------------------
// BloomSettings.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

namespace Revvolvver
{
    /// <summary>
    /// Class holds all the settings used to tweak the bloom effect.
    /// </summary>
    public class GameSettings
    {
        #region Fields

        public  string Name;
        public  float DefaultAmount;
        public  float MinAmount;
        public  float MaxAmount;
        public float Increment;
        public float GameValue;

        #endregion


        /// <summary>
        /// Constructs a new bloom settings descriptor.
        /// </summary>
        public GameSettings(string name, 
                            float gameValue,
                            float defaultAmount, 
                            float minAmount,
                            float maxAmount, 
                            float increment)
        {
            Name = name;
            DefaultAmount = defaultAmount;
            MinAmount = minAmount;
            MaxAmount = maxAmount;
            Increment = increment;
            GameValue = gameValue;

        }


        ///// <summary>
        ///// Table of preset bloom settings, used by the sample program.
        ///// </summary>
        //public static GameSettings[] PresetSettings =
        //{
        //    //                Name          
        //    new GameSettings("Default",     1),

        //};
    }
}
