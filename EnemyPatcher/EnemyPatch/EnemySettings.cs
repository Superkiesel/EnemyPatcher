using BepInEx.Configuration;

namespace EnemyPitchPatch
{
    public static class EnemyPitchSettings
    {
        // Individual enemy pitch values. -1 keeps the original pitch.
        public static ConfigEntry<float> HoarderBug { get; private set; }
        public static ConfigEntry<float> Bracken { get; private set; }
        public static ConfigEntry<float> CoilHead { get; private set; }
        public static ConfigEntry<float> Jester { get; private set; }
        public static ConfigEntry<float> GhostGirl { get; private set; }
        public static ConfigEntry<float> Masked { get; private set; }
        public static ConfigEntry<float> Barber { get; private set; }
        public static ConfigEntry<float> Nutcracker { get; private set; }
        public static ConfigEntry<float> Thumper { get; private set; }
        public static ConfigEntry<float> Spider { get; private set; }
        public static ConfigEntry<float> SnareFlea { get; private set; }
        public static ConfigEntry<float> EarthLeviathan { get; private set; }
        public static ConfigEntry<float> ForestKeeper { get; private set; }
        public static ConfigEntry<float> EyelessDog { get; private set; }
        public static ConfigEntry<float> BaboonHawk { get; private set; }
        public static ConfigEntry<float> KidnapperFox { get; private set; }
        public static ConfigEntry<float> Butler { get; private set; }
        public static ConfigEntry<float> TulipSnake { get; private set; }
        public static ConfigEntry<float> Manticoil { get; private set; }
        public static ConfigEntry<float> CircuitBees { get; private set; }
        public static ConfigEntry<float> Locusts { get; private set; }

        // Timer overrides. -1 keeps the original game value.
        public static ConfigEntry<float> BarberStepInterval { get; private set; }
        public static ConfigEntry<float> JesterWindupTime { get; private set; }

        public static ConfigEntry<float> MaskedApproachDuration { get; private set; }
        public static ConfigEntry<float> MaskedGlowDuration { get; private set; }
        public static ConfigEntry<float> MaskedInfectionDuration { get; private set; }

        public static void Initialize(ConfigFile config)
        {
            const string pitch = "Enemy Pitch";

            HoarderBug = BindPitch(config, pitch, "HoarderBug");
            Bracken = BindPitch(config, pitch, "Bracken");
            CoilHead = BindPitch(config, pitch, "CoilHead");
            Jester = BindPitch(config, pitch, "Jester");
            GhostGirl = BindPitch(config, pitch, "GhostGirl");
            Masked = BindPitch(config, pitch, "Masked");
            Barber = BindPitch(config, pitch, "Barber");
            Nutcracker = BindPitch(config, pitch, "Nutcracker");
            Thumper = BindPitch(config, pitch, "Thumper");
            Spider = BindPitch(config, pitch, "Spider");
            SnareFlea = BindPitch(config, pitch, "SnareFlea");
            EarthLeviathan = BindPitch(config, pitch, "EarthLeviathan");
            ForestKeeper = BindPitch(config, pitch, "ForestKeeper");
            EyelessDog = BindPitch(config, pitch, "EyelessDog");
            BaboonHawk = BindPitch(config, pitch, "BaboonHawk");
            KidnapperFox = BindPitch(config, pitch, "KidnapperFox");
            Butler = BindPitch(config, pitch, "Butler");
            TulipSnake = BindPitch(config, pitch, "TulipSnake");
            Manticoil = BindPitch(config, pitch, "Manticoil");
            CircuitBees = BindPitch(config, pitch, "CircuitBees");
            Locusts = BindPitch(config, pitch, "Locusts");

            const string timers = "Enemy Timers";

            BarberStepInterval = config.Bind(
                timers,
                "BarberStepInterval",
                -1f,
                "Clay Surgeon step interval in seconds. -1 keeps the original.");

            JesterWindupTime = config.Bind(
                timers,
                "JesterWindupTime",
                -1f,
                "Jester windup duration in seconds. -1 keeps the original.");

            const string masked = "Masked Infection";

            MaskedApproachDuration = config.Bind(
                masked,
                "ApproachDuration",
                -1f,
                "Override the original 0.3 second wait. -1 keeps the original.");

            MaskedGlowDuration = config.Bind(
                masked,
                "GlowDuration",
                -1f,
                "Override the original 1.2 second wait. -1 keeps the original.");

            MaskedInfectionDuration = config.Bind(
                masked,
                "InfectionDuration",
                -1f,
                "Override the original 1.5 second wait. -1 keeps the original.");
        }

        private static ConfigEntry<float> BindPitch(
            ConfigFile config,
            string section,
            string key)
        {
            return config.Bind(
                section,
                key,
                -1f,
                "Audio pitch multiplier. -1 keeps the original pitch; any other value sets the pitch.");
        }
    }
}
