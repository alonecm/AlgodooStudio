using AlgodooStudio.Interface;

namespace AlgodooStudio.Phun
{
    /// <summary>
    /// Algodoo设定
    /// </summary>
    public sealed class AlgodooSetting
    {
        /// <summary>
        /// 应用设置
        /// </summary>
        public App AppSetting { get; set; }

        /// <summary>
        /// 控制台设置
        /// </summary>
        public Console ConsoleSetting { get; set; }

        /// <summary>
        /// 默认值集合
        /// </summary>
        public Default DefaultSetting { get; set; }

        /// <summary>
        /// 实体修改器设定
        /// </summary>
        public EntityModifier EntityModifierSetting { get; set; }

        /// <summary>
        /// 实体模型设定
        /// </summary>
        public EntityPrototype EntityPrototypeSetting { get; set; }

        /// <summary>
        /// 界面设定
        /// </summary>
        public GUI GUISetting { get; set; }

        /// <summary>
        /// 输入设定
        /// </summary>
        public Input InputSetting { get; set; }

        /// <summary>
        /// 语言设定
        /// </summary>
        public Language LanguageSetting { get; set; }

        /// <summary>
        /// 当前使用的场景模板
        /// </summary>
        public Palette PaletteSetting { get; set; }

        /// <summary>
        /// 渲染器设定
        /// </summary>
        public Renderer RendererSetting { get; set; }

        /// <summary>
        /// 渲染资源设定
        /// </summary>
        public Resources ResourcesSetting { get; set; }

        /// <summary>
        /// 场景设定
        /// </summary>
        public Scene SceneSetting { get; set; }

        /// <summary>
        /// 拟真设定
        /// </summary>
        public Sim SimSetting { get; set; }

        /// <summary>
        /// 粒子流体动力学求解器设定
        /// </summary>
        public SPH SPHSetting { get; set; }

        /// <summary>
        /// 系统设定
        /// </summary>
        public System SystemSetting { get; set; }

        /// <summary>
        /// 触摸板设定
        /// </summary>
        public Tablet TabletSetting { get; set; }

        /// <summary>
        /// 线程设定
        /// </summary>
        public Threading ThreadingSetting { get; set; }

        /// <summary>
        /// 工具设定
        /// </summary>
        public Tools ToolsSetting { get; set; }

        /// <summary>
        /// 创建一个Algodoo设定
        /// </summary>
        public AlgodooSetting()
        {
            AppSetting = new App();
            ConsoleSetting = new Console();
            PaletteSetting = new Palette();
            GUISetting = new GUI();
            InputSetting = new Input();
            LanguageSetting = new Language();
            EntityModifierSetting = new EntityModifier();
            EntityPrototypeSetting = new EntityPrototype();
            RendererSetting = new Renderer();
            ResourcesSetting = new Resources();
            SceneSetting = new Scene();
            SimSetting = new Sim();
            SPHSetting = new SPH();
            SystemSetting = new System();
            TabletSetting = new Tablet();
            ThreadingSetting = new Threading();
            ToolsSetting = new Tools();
            DefaultSetting = new Default();
        }

        /// <summary>
        /// 应用设置
        /// </summary>
        public sealed class App
        {
            public GUI gui { get; set; }
            public Background background { get; set; }
            public Grid grid { get; set; }

            public App()
            {
                gui = new App.GUI();
                background = new App.Background();
                grid = new App.Grid();
            }

            public int DPI { get; set; }
            public bool HQPolygons { get; set; }
            public bool alwaysCalcContacts { get; set; }
            public bool alwaysCalculateForces { get; set; }
            public bool autosaveEnable { get; set; }
            public int autosaveTime { get; set; }
            public int borderSelectedWidthFactor { get; set; }
            public float borderWidth { get; set; }
            public int chainDensityFactor { get; set; }
            public bool changeSimWhenVisualizing { get; set; }
            public string currentPalette { get; set; }
            public bool drawBCs { get; set; }
            public bool drawBodyCenters { get; set; }
            public bool drawCables { get; set; }
            public bool drawClipPlanes { get; set; }
            public bool drawCollisions { get; set; }
            public bool drawMapOBBs { get; set; }
            public bool drawOBBs { get; set; }
            public bool drawParticleCenters { get; set; }
            public bool drawParticleNeighbors { get; set; }
            public bool drawScaleIndicator { get; set; }
            public bool drawSelectionOBB { get; set; }
            public float drawVelocityFactor { get; set; }
            public bool drawVertices { get; set; }
            public bool enableScriptMenu { get; set; }
            public bool enableUndoMerge { get; set; }
            public float fadeAnglePickerTime { get; set; }
            public float fadeRotate { get; set; }
            public float[] fadeTranslate { get; set; }
            public bool forceVertexPolygonDrawing { get; set; }
            public bool groundPlane { get; set; }
            public bool hideWelcomeScreen { get; set; }
            public float[] iceColorMultiplier { get; set; }
            public bool killerPlanes { get; set; }
            public string language { get; set; }
            public bool laserBroadPhase { get; set; }
            public bool laserEvents { get; set; }
            public float laserFuzziness { get; set; }
            public int laserResolution { get; set; }
            public int laserSuperBoost { get; set; }
            public bool laserWaterBVH { get; set; }
            public float laserWidth { get; set; }
            public int lineWidth { get; set; }
            public int marchingSquaresResolution { get; set; }
            public float maxBorderArea { get; set; }
            public float maxBorderAreaSpecial { get; set; }
            public int maxCogs { get; set; }
            public int maxPlotPoints { get; set; }
            public float maxPointDist { get; set; }
            public float maxPolygonBorderFactor { get; set; }
            public int maxSPHSpawn { get; set; }
            public int maxTracerLife { get; set; }
            public int maxUndo { get; set; }
            public float metaCutoff { get; set; }
            public float minPointDist { get; set; }
            public bool moveToTrashOnOverwrite { get; set; }
            public int numColorsInRainbow { get; set; }
            public int pointSize { get; set; }
            public float[] polytoolPreviewColor { get; set; }
            public float rainbowSplitMult { get; set; }
            public bool scaleGravityField { get; set; }
            public int scalePowAttraction { get; set; }
            public int scalePowControllerAcc { get; set; }
            public int scalePowHingeImpulseBreakLimit { get; set; }
            public float scalePowMotorSpeed { get; set; }
            public int scalePowMotorTorque { get; set; }
            public float scalePowSpringDamping { get; set; }
            public int scalePowSpringStrength { get; set; }
            public int scalePowThrusterForce { get; set; }
            public int sceneFadeInTime { get; set; }
            public bool screenshotsWithAlpha { get; set; }
            public bool showGravityField { get; set; }
            public float timeUntilRealTool { get; set; }
            public float tracerFuzziness { get; set; }
            public bool tracerOverdraw { get; set; }
            public bool transformAttribs { get; set; }
            public int updateInterval { get; set; }
            public bool useLaserFuzzNoShaders { get; set; }
            public float waterElongation { get; set; }
            public int waterFanceyFactor { get; set; }
            public int waterMaxElongation { get; set; }
            public int waterReflectiveness { get; set; }
            public float waterRefractiveIndex { get; set; }
            public float waterTracerLightness { get; set; }
            public float waterTracerSize { get; set; }
            public float waterTracers { get; set; }

            /// <summary>
            /// 背景
            /// </summary>
            public sealed class Background
            {
                public float cloudOpacity { get; set; }
                public bool drawClouds { get; set; }
                public bool fadeCloudsOnSimStop { get; set; }
            }

            /// <summary>
            /// GUI
            /// </summary>
            public sealed class GUI
            {
                public float geomGen_MaxBorderAllowance { get; set; }
                public float geomGen_MinDeltaTolerance { get; set; }
                public float geomGen_MinTolerance { get; set; }
                public bool geomGen_RecognizeMaterial { get; set; }
                public float geomGen_RecognizeMaterial_Cutoff { get; set; }
                public float geomGen_RecognizeMaterial_VarianceFactor { get; set; }
                public bool geomGen_RemoveOpaqueHoles { get; set; }
                public int geomGen_Resolution { get; set; }
                public bool geomGen_SmartBorderColorChoice { get; set; }
                public float geomGen_Tolerance { get; set; }
                public float infoUpdateInterval { get; set; }
                public bool instaErase { get; set; }
                public float momFitFactor { get; set; }
                public bool mouseOverHighlight { get; set; }
                public int panFactor { get; set; }
                public bool playMode { get; set; }
                public bool preScaleBrowserThumbnails { get; set; }
                public bool pressAndHoldMenus { get; set; }
                public bool rotateBackground { get; set; }
                public bool scrollZoom { get; set; }
                public float scrollZoomFactor { get; set; }
                public float selectFactor { get; set; }
                public float sketchMorphDelay { get; set; }
                public float sketchMorphTime { get; set; }
                public int smoothDist { get; set; }
                public int toolGestureSensitivity { get; set; }
                public bool toolGestures { get; set; }
                public bool toolSpecificCursors { get; set; }
                public bool trapUser { get; set; }
                public bool useHSL { get; set; }
                public float velFitFactor { get; set; }

                /// <summary>
                /// 力显示
                /// </summary>
                public sealed class Forces
                {
                    public bool airBuoyancy { get; set; }
                    public string airBuoyancyText { get; set; }
                    public bool airFriction { get; set; }
                    public string airFrictionText { get; set; }
                    public int angMomScale { get; set; }
                    public bool angularMomentum { get; set; }
                    public string angularMomentumText { get; set; }
                    public bool attraction { get; set; }
                    public string attractionText { get; set; }
                    public bool chain { get; set; }
                    public string chainText { get; set; }
                    public float contactCombinationDistance { get; set; }
                    public bool drawAngles { get; set; }
                    public bool drawComponents { get; set; }
                    public bool drawForces { get; set; }
                    public bool drawMomentums { get; set; }
                    public bool drawNames { get; set; }
                    public bool drawValues { get; set; }
                    public bool external { get; set; }
                    public string externalText { get; set; }
                    public float forceScale { get; set; }
                    public bool friction { get; set; }
                    public bool frictionProjection { get; set; }
                    public string frictionText { get; set; }
                    public bool gravity { get; set; }
                    public string gravityText { get; set; }
                    public bool hasAngMomScale { get; set; }
                    public bool hasAngVelScale { get; set; }
                    public bool hasForceScale { get; set; }
                    public bool hasMomScale { get; set; }
                    public bool hasTorqueScale { get; set; }
                    public bool hasVelScale { get; set; }
                    public bool hinge { get; set; }
                    public string hingeText { get; set; }
                    public string linearMomentumText { get; set; }
                    public float maxArrowLength { get; set; }
                    public int momScale { get; set; }
                    public bool normal { get; set; }
                    public string normalText { get; set; }
                    public bool rotation { get; set; }
                    public bool spring { get; set; }
                    public bool thruster { get; set; }
                    public string thrusterText { get; set; }
                    public bool torque { get; set; }
                    public float torqueScale { get; set; }
                    public string torqueText { get; set; }
                    public bool total { get; set; }
                    public string totalText { get; set; }
                    public int velScale { get; set; }
                    public bool velocities { get; set; }
                    public float alikeTolerance { get; set; }
                    public bool allowDrawSelect { get; set; }
                    public bool arrowKeysMove { get; set; }
                    public bool autoFit { get; set; }
                    public bool autoScaleBoxText { get; set; }
                    public int blurMaxDraw { get; set; }
                    public int blurSpacing { get; set; }
                    public bool canPan { get; set; }
                    public bool ctrlSelection { get; set; }
                    public bool dragUndo { get; set; }
                    public bool drawHingesWhenRunning { get; set; }
                    public bool drawTouches { get; set; }
                    public bool dropTexGeomGen { get; set; }
                    public bool fastSketchToolPreview { get; set; }
                    public bool fixedContextMenu { get; set; }
                    public bool fontMenuPreview { get; set; }
                    public float forceFitFactor { get; set; }
                    public int geomGen_Cutoff { get; set; }
                    public float geomGen_Grow { get; set; }
                }
            }

            /// <summary>
            /// 网格线
            /// </summary>
            public sealed class Grid
            {
                public int Base { get; set; }
                public bool grid { get; set; }
                public int numAxes { get; set; }
                public int opacity { get; set; }
                public int scale { get; set; }
                public bool snap { get; set; }
                public float topness { get; set; }
            }
        }

        /// <summary>
        /// 控制台
        /// </summary>
        public sealed class Console
        {
            public float[] color { get; set; }
            public float delay { get; set; }
            public bool fade { get; set; }
            public int maxCharacters { get; set; }
            public float screenSize { get; set; }
            public bool scroll { get; set; }
        }

        /// <summary>
        /// 默认值集合
        /// </summary>
        public sealed class Default
        {
            public bool glueToWorld { get; set; }
            public bool drawCircleCakes { get; set; }
            public bool drawClouds { get; set; }
        }

        /// <summary>
        /// 实体修改器
        /// </summary>
        public sealed class EntityModifier
        {
            public bool blackIsFixed { get; set; }
            public float[] blueColor { get; set; }
            public bool blueIsWater { get; set; }
            public bool useNonPenInputColor { get; set; }
            public bool usePenColorOnEntities { get; set; }
        }

        /// <summary>
        /// 实体模型
        /// </summary>
        public sealed class EntityPrototype
        {
            public float adhesion { get; set; }
            public int airFrictionMult { get; set; }
            public float attraction { get; set; }
            public int attractionType { get; set; }
            public int density { get; set; }
            public bool drawCake { get; set; }
            public float friction { get; set; }
            public bool immortal { get; set; }
            public bool killer { get; set; }
            public bool opaqueBorders { get; set; }
            public float refractiveIndex { get; set; }
            public float restitution { get; set; }
        }

        /// <summary>
        /// GUI
        /// </summary>
        public sealed class GUI
        {
            /// <summary>
            /// 软件外观
            /// </summary>
            public Skin skin { get; set; }

            public GUI()
            {
                skin = new GUI.Skin();
            }

            public bool cursor { get; set; }
            public int cursorFPSLimit { get; set; }
            public bool debugBuffering { get; set; }
            public bool debugExpandables { get; set; }
            public bool debugGUI { get; set; }
            public string defaultExpandOrder { get; set; }
            public bool forceOSCursor { get; set; } = true;
            public float invalidateExtraTime { get; set; }
            public float invalidateMinTime { get; set; }
            public bool lockGUI { get; set; }
            public int maxClickDistance { get; set; }
            public float maxClickTime { get; set; }
            public float maxDoubleClickTime { get; set; }
            public int maxSloppyClickDistance { get; set; }
            public string maxSloppyClickTime { get; set; }
            public bool penInput { get; set; }
            public bool pixelSnap { get; set; }
            public float pixelSnapOffset { get; set; }
            public bool rightToLeft { get; set; }
            public int scale { get; set; }
            public float sliderLabelPrecisionOffset { get; set; }
            public int tabletDistanceSlack { get; set; }
            public int tabletTimeSlack { get; set; }
            public int throwMinSamples { get; set; }
            public float throwMinTime { get; set; }
            public float throwTime { get; set; }
            public bool tooltips { get; set; }
            public int touchDistanceSlack { get; set; }
            public float touchTimeSlack { get; set; }
            public bool useBuffering { get; set; }

            /// <summary>
            /// 软件外观
            /// </summary>
            public sealed class Skin
            {
                public int colorSliderHeight { get; set; }
                public int colorSliderWidth { get; set; }
                public int editMenuDetachThreshold { get; set; }
                public int expandAnimation { get; set; }
                public bool expandButtonClickable { get; set; }
                public float fadeTime { get; set; }
                public bool fontShadow { get; set; }
                public float[] fontShadowOffsetPx { get; set; }
                public int fontSize { get; set; }
                public float iconGrayness { get; set; }
                public float iconLabelDistance { get; set; }
                public int largeIconSize { get; set; }
                public string listPanelFontSize { get; set; }
                public float mouseOverFadeIn { get; set; }
                public float mouseOverFadeOut { get; set; }
                public float opacity { get; set; }
                public int scale { get; set; }
                public int sideMenuDetachThreshold { get; set; }
                public int sliderHeight { get; set; }
                public int sliderWidth { get; set; }
                public int smallIconSize { get; set; }
                public int smallSliderWidth { get; set; }
                public int studentScale { get; set; }
                public int tinyIconSize { get; set; }
                public int toolIconSize { get; set; }
                public int tooltipFontSize { get; set; }
                public int tooltipOpacity { get; set; }
                public int windowDragThreshold { get; set; }
                public int windowFriction { get; set; }
                public int windowMinVel { get; set; }
                public int windowSnapDist { get; set; }
                public int windowSnappedSpacing { get; set; }
                public int windowTitleFontSize { get; set; }
                public int wobbleFriction { get; set; }
                public int wobbleStiffness { get; set; }
            }
        }

        /// <summary>
        /// 输入设置
        /// </summary>
        public sealed class Input
        {
            public bool fingerShift { get; set; }
            public float inputJitter { get; set; }
            public bool moveWithFinger { get; set; }
            public bool multiPointerContac { get; set; }
            public bool noMouseInput { get; set; }
            public bool spam { get; set; }
            public bool table { get; set; }
            public bool touchPad { get; set; }
            public float touchPanCutoff { get; set; }
            public int touchPanSensitivity { get; set; }
            public float touchRotateCutoff { get; set; }
            public bool touchScreenDetection { get; set; }
            public int touchScreenDetectionGoodCutoff { get; set; }
            public int touchScreenDetectionRelCutoff { get; set; }
            public int touchZoomCutoff { get; set; }
            public bool trackSMART { get; set; }
        }

        /// <summary>
        /// 语言
        /// </summary>
        public sealed class Language
        {
            public string[] fallbackFontName { get; set; }
            public string[] fallbackFontNameFixed { get; set; }
            public int fontScale { get; set; }
            public float metaVersion { get; set; }
            public bool rightToLeft { get; set; }
        }

        /// <summary>
        /// 场景模板
        /// </summary>
        public sealed class Palette
        {
            public bool borders { get; set; }
            public int[][] colorRangesHSVA { get; set; }
            public bool drawCircleCakes { get; set; }
            public bool drawClouds { get; set; }
            public bool opaqueBorders { get; set; }
            public bool protractor { get; set; }
            public bool ruler { get; set; }
            public bool showForces { get; set; }
            public bool showMomentums { get; set; }
            public bool showVelocities { get; set; }
        }

        /// <summary>
        /// 渲染器
        /// </summary>
        public sealed class Renderer
        {
            public int batchExtensiveCutoff { get; set; }
            public int batchHGcutoff { get; set; }
            public int batchHGdepth { get; set; }
            public int batchHGsizeBits { get; set; }
            public string batchMethod { get; set; }
            public int batchNPcutoff { get; set; }
            public bool batchNarrowPhase { get; set; }
            public int batchQTdepth { get; set; }
            public int batchRGsize { get; set; }
            public bool batchSpam { get; set; }
            public bool clampTC { get; set; }
            public bool colorAttrib8bit { get; set; }
            public bool debugOverdraw { get; set; }
            public float[] debugOverdrawColor { get; set; }
        }

        /// <summary>
        /// 渲染资源
        /// </summary>
        public sealed class Resources
        {
            public bool allowNPOT { get; set; }
            public bool fontAA { get; set; }
            public string[] fontFallbacks { get; set; }
            public int fontHint { get; set; }
            public bool fontMetrics { get; set; }
            public bool forceGLUBuildMipmaps { get; set; }
            public int maxAnisotropy { get; set; }
            public int maxDesiredTextureSizeSide { get; set; }
            public string sRGB { get; set; }
            public bool textures { get; set; }
        }

        /// <summary>
        /// 粒子流体动力学求解器
        /// </summary>
        public sealed class SPH
        {
            public int bucketSize { get; set; }
            public bool dens_constraint { get; set; }
            public int density { get; set; }
            public bool directSolver { get; set; }
            public float friction { get; set; }
            public float geomFieldOffset { get; set; }
            public float geomFriction { get; set; }
            public int geomMassMultiplier { get; set; }
            public bool geometryFields { get; set; }
            public bool incompressible { get; set; }
            public float influence { get; set; }
            public float jitter { get; set; }
            public float kernelMultiplier { get; set; }
            public bool kernelNormalize { get; set; }
            public float lowMassCollisionRadiusFactor { get; set; }
            public int maxNeighbors { get; set; }
            public int minMassFactor { get; set; }
            public bool nonPenetration { get; set; }
            public int pressMultiplier { get; set; }
            public float radius { get; set; }
            public float restitution { get; set; }
            public int solveIters { get; set; }
            public int solveSoftness { get; set; }
            public float solveT_dens { get; set; }
            public bool sort { get; set; }
            public int soundSpeed { get; set; }
            public bool specialPressure { get; set; }
            public bool spikyKernel { get; set; }
            public float surfaceTension { get; set; }
            public bool unilateral { get; set; }
            public string vaporizeTime { get; set; }
            public bool velocitySmoothing { get; set; }
            public int viscMultiplier { get; set; }
            public float viscosity { get; set; }
        }

        /// <summary>
        /// 场景
        /// </summary>
        public sealed class Scene
        {
            public Camera camera { get; set; }
            public bool controllAccelerationFollowCamera { get; set; }
            public string gravityRotationOffset { get; set; }

            public Scene()
            {
                camera = new Scene.Camera();
            }

            /// <summary>
            /// 摄像机
            /// </summary>
            public sealed class Camera
            {
                public bool kineticPanning { get; set; }
                public float[] pan { get; set; }
                public float rotation { get; set; }
                public float smoothFactor { get; set; }
                public bool smoothPan { get; set; }
                public bool smoothRotation { get; set; }
                public bool smoothZoom { get; set; }
                public bool suspendFollowIfTooling { get; set; }
                public bool trackRotation { get; set; }
                public int zoom { get; set; }
            }
        }

        /// <summary>
        /// 拟真
        /// </summary>
        public sealed class Sim
        {
            public bool adHocSolver { get; set; }
            public float airDensity { get; set; }
            public float airFrictionLinear { get; set; }
            public int airFrictionMultiplier { get; set; }
            public float airFrictionQuadratic { get; set; }
            public int airFrictionVersion { get; set; }
            public bool airSwitch { get; set; }
            public bool blockedContactSolver { get; set; }
            public bool blockedContactSolver2N { get; set; }
            public bool blockedContactSolver2NF { get; set; }
            public bool blockedContactSolver3N { get; set; }
            public bool blockedContactSolver4N { get; set; }
            public float bruteColliderCutoff { get; set; }
            public bool cableAdaptiveSlack { get; set; }
            public bool cableBendConstraint { get; set; }
            public int cableBendStrength { get; set; }
            public bool cableDistanceConstraint { get; set; }
            public bool cableExtrudeContacts { get; set; }
            public int cableJacobianScale { get; set; }
            public bool cableLineConstraint { get; set; }
            public bool cableLineInternalize { get; set; }
            public bool cableLineSmartChoice { get; set; }
            public string cableMaxImpFactor { get; set; }
            public int cableMaxMassRation { get; set; }
            public float cableMaxSlack { get; set; }
            public bool cableMiliatryConstraint { get; set; }
            public float cableSafetyDistance { get; set; }
            public bool cables { get; set; }
            public bool collideCallbacksEveryStep { get; set; }
            public bool debugCables { get; set; }
            public bool debugSolver { get; set; }
            public bool directContactSolveAll { get; set; }
            public bool directContactSolveChains { get; set; }
            public int directContactSolveChainsMassRatioLimit { get; set; }
            public bool directDragSolve { get; set; }
            public bool directHingeSolve { get; set; }
            public int directSolveIters { get; set; }
            public bool directSpringSolve { get; set; }
            public bool direct_friction { get; set; }
            public bool direct_lcp { get; set; }
            public bool dsFirst { get; set; }
            public bool dsLast { get; set; }
            public bool fastPolyAABB { get; set; }
            public bool forceContactCalculations { get; set; }
            public int frequency { get; set; }
            public bool geomAttraction { get; set; }
            public float gravityAngleOffset { get; set; }
            public float gravityStrength { get; set; }
            public bool gravitySwitch { get; set; }
            public float hingeAngleConstraintType { get; set; }
            public bool hingeAutoBend { get; set; }
            public bool hingeAutoBendIsHolonomic { get; set; }
            public float hingeAutoBend_cutoff { get; set; }
            public float hingeAutoBend_dampingFactor { get; set; }
            public bool hingeAutoBend_unilateral { get; set; }
            public float impactCutoff { get; set; }
            public float initialFriction { get; set; }
            public bool iterativeContactsToo { get; set; }
            public bool iterativeHingesToo { get; set; }
            public bool iterativeSpringsToo { get; set; }
            public bool largeOverlapTest { get; set; }
            public int legacyMode { get; set; }
            public float limitAngVel { get; set; }
            public double limitPos { get; set; }
            public double limitVel { get; set; }
            public int maxPositionCorrection { get; set; }
            public int maxSpringStrength { get; set; }
            public int mlcp_maxIter { get; set; }
            public float mlcp_tolerance { get; set; }
            public bool multipleContactEventPerPair { get; set; }
            public int nyquistFactor { get; set; }
            public bool optimizeContactSet { get; set; }
            public bool positionsLast { get; set; }
            public bool preApplyForces { get; set; }
            public bool projectContacts { get; set; }
            public bool pureIterativeFinish { get; set; }
            public bool regularizeBounces { get; set; }
            public float rotFrictionLinear { get; set; }
            public bool running { get; set; }
            public bool sabreSetNumBodies { get; set; }
            public int sabreThreads { get; set; }
            public bool scriptUpdatesEveryStep { get; set; }
            public bool separatingContactsBounce { get; set; }
            public float skipDistance { get; set; }
            public int solveAccFactor { get; set; }
            public int solveDistFactor { get; set; }
            public int solveIter { get; set; }
            public bool solvePenetrationDamping { get; set; }
            public bool solvePreSortConstraints { get; set; }
            public bool solveRandomizeConstraints { get; set; }
            public int solveRegularizationFactor { get; set; }
            public bool solveReverseConstraints { get; set; }
            public float solveStiffIter { get; set; }
            public float solveTCables { get; set; }
            public float solveTconstraints { get; set; }
            public float solveTcontacts { get; set; }
            public int solveVelFactor { get; set; }
            public bool sortBroadPhasePairs { get; set; }
            public int springForce { get; set; }
            public float stepsPerFrame { get; set; }
            public int timeFactor { get; set; }
            public bool useSkipLists { get; set; }
            public bool warmStart { get; set; }
            public float warmStartFactor { get; set; }
            public bool warmStartProgressive { get; set; }
            public float warmStartProgressiveCutoff { get; set; }
            public int warmStartProgressiveMax { get; set; }
            public float windAngle { get; set; }
            public float windStrength { get; set; }
        }

        /// <summary>
        /// 系统
        /// </summary>
        public sealed class System
        {
            public int antiAlias { get; set; }
            public int depth { get; set; }
            public bool fullscreen { get; set; }
            public int maxFPSPaused { get; set; }
            public int maxFPSPlaying { get; set; }
            public bool maximized { get; set; }
            public int minFPS { get; set; }
            public float[] position { get; set; }
            public float regularScreenshots { get; set; }
            public float[] resolution { get; set; }
            public int stencilDepth { get; set; }
            public int zDepth { get; set; }
        }

        /// <summary>
        /// 平板设定
        /// </summary>
        public sealed class Tablet
        {
            public bool improveClickTolerance { get; set; }
            public bool overrideMouseEvents { get; set; }
            public bool rightClickOverride { get; set; }
        }

        /// <summary>
        /// 线程
        /// </summary>
        public sealed class Threading
        {
            /// <summary>
            /// 线程数
            /// </summary>
            public int NumThread { get; set; }
        }

        /// <summary>
        /// 工具
        /// </summary>
        public sealed class Tools
        {
            public BoxTool boxTool { get; set; }
            public BrushTool brushTool { get; set; }
            public CSGTool csgTool { get; set; }
            public ChainTool chainTool { get; set; }
            public CircleTool circleTool { get; set; }
            public CutTool cutTool { get; set; }
            public DragTool dragTool { get; set; }
            public EraserTool eraserTool { get; set; }
            public FixJointTool fixJointTool { get; set; }
            public GearTool gearTool { get; set; }
            public HingeTool hingeTool { get; set; }
            public MoveTool moveTool { get; set; }
            public MultitouchGeometryTool multitouchGeometryTool { get; set; }
            public PlaneTool planeTool { get; set; }
            public RotateTool rotateTool { get; set; }
            public SketchTool sketchTool { get; set; }
            public SpringTool springTool { get; set; }
            public TextureTool textureTool { get; set; }
            public TracerTool tracerTool { get; set; }

            public Tools()
            {
                boxTool = new Tools.BoxTool();
                brushTool = new Tools.BrushTool();
                csgTool = new Tools.CSGTool();
                chainTool = new Tools.ChainTool();
                circleTool = new Tools.CircleTool();
                cutTool = new Tools.CutTool();
                dragTool = new Tools.DragTool();
                eraserTool = new Tools.EraserTool();
                fixJointTool = new Tools.FixJointTool();
                gearTool = new Tools.GearTool();
                hingeTool = new Tools.HingeTool();
                moveTool = new Tools.MoveTool();
                multitouchGeometryTool = new Tools.MultitouchGeometryTool();
                planeTool = new Tools.PlaneTool();
                rotateTool = new Tools.RotateTool();
                sketchTool = new Tools.SketchTool();
                springTool = new Tools.SpringTool();
                textureTool = new Tools.TextureTool();
                tracerTool = new Tools.TracerTool();
            }

            public sealed class BoxTool : IHotkey
            {
                public string hotkey { get; set; }
            }

            public sealed class BrushTool : IHotkey
            {
                public bool autoGlue { get; set; }
                public bool calligraphy { get; set; }
                public bool clickDraw { get; set; }
                public bool drawInstaEraser { get; set; }
                public bool drawShape { get; set; }
                public bool freezeDrawing { get; set; }
                public string hotkey { get; set; }
                public bool overdraw { get; set; }
                public bool planeGlue { get; set; }
                public bool pressureSensitive { get; set; }
                public float size { get; set; }
                public bool useTouchSize { get; set; }
            }

            public sealed class CSGTool
            {
                public int scale { get; set; }
            }

            public sealed class ChainTool : IHotkey
            {
                public bool groupify { get; set; }
                public string hotkey { get; set; }
                public float linkDistance { get; set; }
                public int linkWidthFactor { get; set; }
                public string selectedTemplate { get; set; }
            }

            public sealed class CircleTool : IHotkey
            {
                public string hotkey { get; set; }
            }

            public sealed class CutTool : IHotkey
            {
                public string hotkey { get; set; }
            }

            public sealed class DragTool : IHotkey
            {
                public bool allowPan { get; set; }
                public bool allowZoom { get; set; }
                public bool centerOfMass { get; set; }
                public bool dragMode { get; set; }
                public bool drawLine { get; set; }
                public bool drawStrength { get; set; }
                public string hotkey { get; set; }
                public string maxForce { get; set; }
                public bool noRot { get; set; }
                public int smartAttachRad { get; set; }
                public int solveD { get; set; }
            }

            public sealed class EraserTool
            {
                public bool autoGlue { get; set; }
                public bool calligraphy { get; set; }
                public bool clickDraw { get; set; }
                public bool drawInstaEraser { get; set; }
                public bool drawShape { get; set; }
                public bool freezeDrawing { get; set; }
                public bool overdraw { get; set; }
                public bool planeGlue { get; set; }
                public bool pressureSensitive { get; set; }
                public float size { get; set; }
                public bool useTouchSize { get; set; }
            }

            public sealed class FixJointTool : IHotkey
            {
                public string hotkey { get; set; }
            }

            public sealed class GearTool : IHotkey
            {
                public float cogSize { get; set; }
                public string hotkey { get; set; }
                public bool inside { get; set; }
                public bool outside { get; set; }
                public float thickness { get; set; }
            }

            public sealed class HingeTool : IHotkey
            {
                public string hotkey { get; set; }
            }

            public sealed class LaserPenTool : IHotkey
            {
                public string hotkey { get; set; }
            }

            public sealed class MoveTool : IHotkey
            {
                public string hotkey { get; set; }
            }

            public sealed class MultitouchBackgroundTool
            {
                public int maxTouchDistance { get; set; }
            }

            public sealed class MultitouchGeometryTool
            {
                public bool axisScale { get; set; }
                public float moveTolerance { get; set; }
                public float rotateTolerance { get; set; }
                public int scaleTolerance { get; set; }
            }

            public sealed class PlaneTool : IHotkey
            {
                public string hotkey { get; set; }
            }

            public sealed class PolyTool : IHotkey
            {
                public string hotkey { get; set; }
            }

            public sealed class RotateTool : IHotkey
            {
                public string hotkey { get; set; }
            }

            public sealed class SketchTool : IHotkey
            {
                public bool enableSelectCircleMouse { get; set; }
                public bool enableSelectCircleTouchScreen { get; set; }
                public string hotkey { get; set; }
            }

            public sealed class SpringTool : IHotkey
            {
                public string hotkey { get; set; }
                public bool previewLength { get; set; }
            }

            public sealed class TextureTool : IHotkey
            {
                public string hotkey { get; set; }
            }

            public sealed class ThrusterTool : IHotkey
            {
                public string hotkey { get; set; }
            }

            public sealed class TracerTool : IHotkey
            {
                public string hotkey { get; set; }
            }
        }
    }
}