﻿namespace F4Utils.SimSupport
{
    public enum F4SimOutputs
    {
        MAP__GROUND_POSITION__FEET_NORTH_OF_MAP_ORIGIN,
        MAP__GROUND_POSITION__FEET_EAST_OF_MAP_ORIGIN,
        MAP__GROUND_SPEED_VECTOR__NORTH_COMPONENT_FPS,
        MAP__GROUND_SPEED_VECTOR__EAST_COMPONENT_FPS,
        MAP__GROUND_SPEED_KNOTS,
        MAP__GROUND_POSITION__LATITUDE,
        MAP__GROUND_POSITION__LONGITUDE,
        TRUE_ALTITUDE__MSL,
        ALTIMETER__INDICATED_ALTITUDE__MSL,
        ALTIMETER__BAROMETRIC_PRESSURE_INCHES_HG,
        VVI__VERTICAL_VELOCITY_FPM,
        FLIGHT_DYNAMICS__SIDESLIP_ANGLE_DEGREES,
        FLIGHT_DYNAMICS__CLIMBDIVE_ANGLE_DEGREES,
        FLIGHT_DYNAMICS__OWNSHIP_NORMAL_GS,
        AIRSPEED_MACH_INDICATOR__MACH_NUMBER,
        AIRSPEED_MACH_INDICATOR__INDICATED_AIRSPEED_KNOTS,
        AIRSPEED_MACH_INDICATOR__TRUE_AIRSPEED_KNOTS,
        HUD__WIND_DELTA_TO_FLIGHT_PATH_MARKER_DEGREES,
        NOZ_POS1__NOZZLE_PERCENT_OPEN,
        NOZ_POS2__NOZZLE_PERCENT_OPEN,
        HYD_PRESSURE_A__PSI,
        HYD_PRESSURE_B__PSI,
        FUEL_QTY__INTERNAL_FUEL_POUNDS,
        FUEL_QTY__EXTERNAL_FUEL_POUNDS,
        FUEL_FLOW1__FUEL_FLOW_POUNDS_PER_HOUR,
        FUEL_FLOW2__FUEL_FLOW_POUNDS_PER_HOUR,
        RPM1__RPM_PERCENT,
        RPM2__RPM_PERCENT,
        FTIT1__FTIT_TEMP_DEG_CELCIUS,
        FTIT2__FTIT_TEMP_DEG_CELCIUS,
        SPEED_BRAKE__POSITION,
        SPEED_BRAKE__NOT_STOWED_FLAG,
        EPU_FUEL__EPU_FUEL_PERCENT,
        OIL_PRESS1__OIL_PRESS_PERCENT,
        OIL_PRESS2__OIL_PRESS_PERCENT,
        CABIN_PRESS__CABIN_PRESS_FEET_MSL,
        COMPASS__MAGNETIC_HEADING_DEGREES,
        GEAR_PANEL__GEAR_POSITION,
        GEAR_PANEL__NOSE_GEAR_DOWN_LIGHT,
        GEAR_PANEL__LEFT_GEAR_DOWN_LIGHT,
        GEAR_PANEL__RIGHT_GEAR_DOWN_LIGHT,
        GEAR_PANEL__NOSE_GEAR_POSITION,
        GEAR_PANEL__LEFT_GEAR_POSITION,
        GEAR_PANEL__RIGHT_GEAR_POSITION,
        GEAR_PANEL__GEAR_HANDLE_LIGHT,
        GEAR_PANEL__PARKING_BRAKE_ENGAGED_FLAG,
        ADI__PITCH_DEGREES,
        ADI__ROLL_DEGREES,
        ADI__ILS_SHOW_COMMAND_BARS,
        ADI__ILS_HORIZONTAL_BAR_POSITION,
        ADI__ILS_VERTICAL_BAR_POSITION,
        ADI__RATE_OF_TURN_INDICATOR_POSITION,
        ADI__INCLINOMETER_POSITION,
        ADI__OFF_FLAG,
        ADI__AUX_FLAG,
        ADI__GS_FLAG,
        ADI__LOC_FLAG,
        STBY_ADI__PITCH_DEGREES,
        STBY_ADI__ROLL_DEGREES,
        STBY_ADI__OFF_FLAG,
        VVI__OFF_FLAG,
        AOA_INDICATOR__AOA_DEGREES,
        AOA_INDICATOR__OFF_FLAG,
        HSI__COURSE_DEVIATION_INVALID_FLAG,
        HSI__DISTANCE_INVALID_FLAG,
        HSI__DESIRED_COURSE_DEGREES,
        HSI__COURSE_DEVIATION_DEGREES,
        HSI__COURSE_DEVIATION_LIMIT_DEGREES,
        HSI__DISTANCE_TO_BEACON_NAUTICAL_MILES,
        HSI__BEARING_TO_BEACON_DEGREES,
        HSI__CURRENT_HEADING_DEGREES,
        HSI__DESIRED_HEADING_DEGREES,
        HSI__LOCALIZER_COURSE_DEGREES,
        MAP__AIRBASE_FEET_NORTH_OF_MAP_ORIGIN,
        MAP__AIRBASE_FEET_EAST_OF_MAP_ORIGIN,
        HSI__TO_FLAG,
        HSI__FROM_FLAG,
        HSI__OFF_FLAG,
        HSI__HSI_MODE,
        TRIM__PITCH_TRIM,
        TRIM__ROLL_TRIM,
        TRIM__YAW_TRIM,
        DED__LINES,
        DED__INVERT_LINES,
        PFL__LINES,
        PFL__INVERT_LINES,
        UFC__TACAN_CHANNEL,
        AUX_COMM__TACAN_CHANNEL,
        AUX_COMM__TACAN_BAND_IS_X,
        AUX_COMM__TACAN_MODE_IS_AA,
        AUX_COMM__UHF_PRESET,
        AUX_COMM__UHF_FREQUENCY,
        UFC__TACAN_BAND_IS_X,
        UFC__TACAN_MODE_IS_AA,
        IFF__BACKUP_MODE_1_DIGIT_1,
        IFF__BACKUP_MODE_1_DIGIT_2,
        IFF__BACKUP_MODE_3A_DIGIT_1,
        IFF__BACKUP_MODE_3A_DIGIT_2,
        FUEL_QTY__FOREWARD_QTY_LBS,
        FUEL_QTY__AFT_QTY_LBS,
        FUEL_QTY__TOTAL_FUEL_LBS,
        LMFD__OSB_LABEL_LINES1,
        LMFD__OSB_LABEL_LINES2,
        LMFD__OSB_INVERTED_FLAGS,
        RMFD__OSB_LABEL_LINES1,
        RMFD__OSB_LABEL_LINES2,
        RMFD__OSB_INVERTED_FLAGS,
        MASTER_CAUTION_LIGHT,
        MASTER_CAUTION_ANNOUNCED,
        LEFT_EYEBROW_LIGHTS__TFFAIL,
        LEFT_EYEBROW_LIGHTS__ALTLOW,
        LEFT_EYEBROW_LIGHTS__OBSWRN,
        RIGHT_EYEBROW_LIGHTS__ENGFIRE,
        RIGHT_EYEBROW_LIGHTS__ENGINE,
        RIGHT_EYEBROW_LIGHTS__HYDOIL,
        RIGHT_EYEBROW_LIGHTS__DUALFC,
        RIGHT_EYEBROW_LIGHTS__FLCS,
        RIGHT_EYEBROW_LIGHTS__CANOPY,
        RIGHT_EYEBROW_LIGHTS__TO_LDG_CONFIG,
        RIGHT_EYEBROW_LIGHTS__OXY_LOW,
        RIGHT_EYEBROW_LIGHTS__DBU_ON,
        CAUTION_PANEL__FLCS_FAULT,
        CAUTION_PANEL__LE_FLAPS,
        CAUTION_PANEL__ELEC_SYS,
        CAUTION_PANEL__ENGINE_FAULT,
        CAUTION_PANEL__SEC,
        CAUTION_PANEL__FWD_FUEL_LOW,
        CAUTION_PANEL__AFT_FUEL_LOW,
        CAUTION_PANEL__OVERHEAT,
        CAUTION_PANEL__BUC,
        CAUTION_PANEL__FUEL_OIL_HOT,
        CAUTION_PANEL__SEAT_NOT_ARMED,
        CAUTION_PANEL__AVIONICS_FAULT,
        CAUTION_PANEL__RADAR_ALT,
        CAUTION_PANEL__EQUIP_HOT,
        CAUTION_PANEL__ECM,
        CAUTION_PANEL__STORES_CONFIG,
        CAUTION_PANEL__ANTI_SKID,
        CAUTION_PANEL__HOOK,
        CAUTION_PANEL__NWS_FAIL,
        CAUTION_PANEL__CABIN_PRESS,
        CAUTION_PANEL__OXY_LOW,
        CAUTION_PANEL__PROBE_HEAT,
        CAUTION_PANEL__FUEL_LOW,
        CAUTION_PANEL__IFF,
        CAUTION_PANEL__C_ADC,
        CAUTION_PANEL__ATF_NOT_ENGAGED,
        AOA_INDEXER__AOA_TOO_HIGH,
        AOA_INDEXER__AOA_IDEAL,
        AOA_INDEXER__AOA_TOO_LOW,
        NWS_INDEXER__RDY,
        NWS_INDEXER__AR_NWS,
        NWS_INDEXER__DISC,
        TWP__HANDOFF,
        TWP__MISSILE_LAUNCH,
        TWP__PRIORITY_MODE,
        TWP__UNKNOWN,
        TWP__NAVAL,
        TWP__TARGET_SEP,
        TWP__SYS_TEST,
        TWA__SEARCH,
        TWA__ACTIVITY_POWER,
        TWA__LOW_ALTITUDE,
        TWA__SYSTEM_POWER,
        ECM__POWER,
        ECM__FAIL,
        MISC__ADV_MODE_ACTIVE,
        MISC__ADV_MODE_STBY,
        MISC__AUTOPILOT_ENGAGED,
        CMDS__CHAFF_COUNT,
        CMDS__FLARE_COUNT,
        CMDS__GO,
        CMDS__NOGO,
        CMDS__AUTO_DEGR,
        CMDS__DISPENSE_RDY,
        CMDS__CHAFF_LO,
        CMDS__FLARE_LO,
        CMDS__MODE,
        ELEC__FLCS_PMG,
        ELEC__MAIN_GEN,
        ELEC__STBY_GEN,
        ELEC__EPU_GEN,
        ELEC__EPU_PMG,
        ELEC__TO_FLCS,
        ELEC__FLCS_RLY,
        ELEC__BATT_FAIL,
        TEST__ABCD,
        EPU__HYDRAZN,
        EPU__AIR,
        EPU__RUN,
        AVTR__AVTR,
        JFS__RUN,
        MARKER_BEACON__MRK_BCN_LIGHT,
        MARKER_BEACON__OUTER_MARKER_FLAG,
        MARKER_BEACON__MIDDLE_MARKER_FLAG,
        AIRCRAFT__ONGROUND,
        AIRCRAFT__MAIN_LANDING_GEAR__WEIGHT_ON_WHEELS,
        AIRCRAFT__NOSE_LANDING_GEAR__WEIGHT_ON_WHEELS,
        POWER__ELEC_POWER_OFF,
        POWER__MAIN_POWER,
        POWER__BUS_POWER_BATTERY,
        POWER__BUS_POWER_EMERGENCY,
        POWER__BUS_POWER_ESSENTIAL,
        POWER__BUS_POWER_NON_ESSENTIAL,
        POWER__BUS_POWER_MAIN_GENERATOR,
        POWER__MAIN_GENERATOR,
        POWER__STANDBY_GENERATOR,
        POWER__JET_FUEL_STARTER,
        RIGHT_EYEBROW_LIGHTS__ENGINE_2_FIRE,
        AIRCRAFT__LEADING_EDGE_FLAPS_POSITION,
        AIRCRAFT__TRAILING_EDGE_FLAPS_POSITION,
        AIRCRAFT__VTOL_POSITION,
        SIM__BMS_PLAYER_IS_FLYING,
        SIM__FLIGHTDATA_VERSION_NUM,
        SIM__FLIGHTDATA2_VERSION_NUM,
        AIRCRAFT__VEHICLE_ACD,
        SIM__CURRENT_TIME,
        SIM__PILOT_CALLSIGN,
        SIM__PILOT_STATUS,
        SIM__AA_MISSILE_FIRED,
        SIM__AG_MISSILE_FIRED,
        SIM__BOMB_DROPPED,
        SIM__FLARE_DROPPED,
        SIM__CHAFF_DROPPED,
        SIM__BULLETS_FIRED,
        SIM__COLLISION_COUNTER,
        SIM__GFORCE,
        SIM__LAST_DAMAGE,
        SIM__DAMAGE_FORCE,
        SIM__WHEN_DAMAGE,
        SIM__EYE_X,
        SIM__EYE_Y,
        SIM__EYE_Z,
        SIM__IS_FIRING_GUN,
        SIM__IS_END_FLIGHT,
        SIM__IS_EJECTING,
        SIM__IN_3D,
        SIM__IS_PAUSED,
        SIM__IS_FROZEN,
        SIM__IS_OVER_G,
        SIM__IS_ON_GROUND,
        SIM__IS_EXIT_GAME,
        SIM__BUMP_INTENSITY,
        INSTR_LIGHTING_INTENSITY,
        RADIO_CLIENT_STATUS__CLIENT_ACTIVE_FLAG,
        RADIO_CLIENT_STATUS__CONNECTED_FLAG,
        RADIO_CLIENT_STATUS__CONNECTION_FAIL_FLAG,
        RADIO_CLIENT_STATUS__HOST_UNKNOWN_FLAG,
        RADIO_CLIENT_STATUS__BAD_PASSWORD_FLAG,
        RADIO_CLIENT_STATUS__NO_MICROPHONE_FLAG,
        RADIO_CLIENT_STATUS__NO_SPEAKERS_FLAG,

        RADIO_CLIENT_CONTROL__UHF_RADIO__FREQUENCY,
        RADIO_CLIENT_CONTROL__UHF_RADIO__RX_VOLUME,
        RADIO_CLIENT_CONTROL__UHF_RADIO__IS_ON_FLAG,
        RADIO_CLIENT_CONTROL__UHF_RADIO__PTT_DEPRESSED_FLAG,

        RADIO_CLIENT_CONTROL__VHF_RADIO__FREQUENCY,
        RADIO_CLIENT_CONTROL__VHF_RADIO__RX_VOLUME,
        RADIO_CLIENT_CONTROL__VHF_RADIO__IS_ON_FLAG,
        RADIO_CLIENT_CONTROL__VHF_RADIO__PTT_DEPRESSED_FLAG,

        RADIO_CLIENT_CONTROL__GUARD_RADIO__FREQUENCY,
        RADIO_CLIENT_CONTROL__GUARD_RADIO__RX_VOLUME,
        RADIO_CLIENT_CONTROL__GUARD_RADIO__IS_ON_FLAG,
        RADIO_CLIENT_CONTROL__GUARD_RADIO__PTT_DEPRESSED_FLAG,

        RADIO_CLIENT_CONTROL__CONNECTION__NICKNAME,
        RADIO_CLIENT_CONTROL__CONNECTION__ADDRESS,
        RADIO_CLIENT_CONTROL__CONNECTION__PORT_NUMBER,
        RADIO_CLIENT_CONTROL__CONNECTION__PASSWORD,
        RADIO_CLIENT_CONTROL__CONNECTION__PLAYER_COUNT,
        RADIO_CLIENT_CONTROL__CONNECTION__SIGNAL_CONNECT_FLAG,
        RADIO_CLIENT_CONTROL__CONNECTION__TERMINATE_CLIENT_FLAG,
        RADIO_CLIENT_CONTROL__CONNECTION__FLIGHT_MODE_FLAG,
        RADIO_CLIENT_CONTROL__CONNECTION__USE_AGC_FLAG,
        RADIO_CLIENT_CONTROL__MAIN_DEVICE__IC_VOLUME,

        PILOT__HEADX_OFFSET,
        PILOT__HEADY_OFFSET,
        PILOT__HEADZ_OFFSET,
        PILOT__HEAD_PITCH_DEGREES,
        PILOT__HEAD_ROLL_DEGREES,
        PILOT__HEAD_YAW_DEGREES,

        FLIGHT_CONTROL__RUN,
        FLIGHT_CONTROL__FAIL,
        RWR__OBJECT_COUNT,
        RWR__SYMBOL_ID,
        RWR__BEARING_DEGREES,
        RWR__MISSILE_ACTIVITY_FLAG,
        RWR__MISSILE_LAUNCH_FLAG,
        RWR__SELECTED_FLAG,
        RWR__LETHALITY,
        RWR__NEWDETECTION_FLAG,
        RWR__ADDITIONAL_INFO
    }
}