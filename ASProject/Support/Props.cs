namespace AlgodooStudio.ASProject.Support
{
    /// <summary>
    /// 实体的基本属性值
    /// </summary>
    public static class Props
    {
        public static readonly string[] circle = {"adhesion","airFrictionMult","angle",
            "angvel","attraction","attractionType","collideSet","collideWater",
            "color","colorHSVA","controllerAcc","controllerInvertX","controllerInvertY",
            "controllerReverseXY","density","drawBorder","drawCake","edgeBlur","friction","glued",
            "heteroCollide","immortal","inertiaMultiplier","Killer","materialVelocity",
            "materialName","onClick","onCollide","onDie","onHitByLaser","onKey","onSpawn",
            "opaqueBorders","pos","postStep","protractor","radius","reflectiveness",
            "refractiveIndex","resources","restitution","showForceArrows","showMomentum",
            "showVelocity","texture","textureClamped","textureMatrix","timetolive","update",
            "vel","velocityDamping","zDepth"
        };

        public static readonly string[] box = {"adhesion","airFrictionMult","angle",
            "angvel","attraction","attractionType","collideSet","collideWater",
            "color","colorHSVA","controllerAcc","controllerInvertX","controllerInvertY",
            "controllerReverseXY","density","drawBorder","edgeBlur","friction","glued",
            "heteroCollide","immortal","inertiaMultiplier","Killer","materialVelocity",
            "materialName","onClick","onCollide","onDie","onHitByLaser","onKey","onSpawn",
            "opaqueBorders","pos","postStep","reflectiveness","refractiveIndex",
            "resources","restitution","ruler","showForceArrows","showMomentum",
            "showVelocity","size","text","textColor","textConstrained","textFont",
            "textFontSize","textScale","texture","textureClamped","textureMatrix","timetolive","update",
            "vel","velocityDamping","zDepth"
        };

        public static readonly string[] polygon = {"adhesion","airFrictionMult","angle",
            "angvel","attraction","attractionType","collideSet","collideWater",
            "color","colorHSVA","controllerAcc","controllerInvertX","controllerInvertY",
            "controllerReverseXY","density","drawBorder","edgeBlur","forceVertexDrawing","friction","glued",
            "heteroCollide","immortal","inertiaMultiplier","Killer","materialVelocity",
            "materialName","onClick","onCollide","onDie","onHitByLaser","onKey","onSpawn",
            "opaqueBorders","pos","postStep","reflectiveness","refractiveIndex",
            "resources","restitution","showForceArrows","showMomentum",
            "showVelocity","texture","textureClamped","textureMatrix","timetolive","update",
            "vel","velocityDamping","zDepth","surfaces"
        };

        public static readonly string[] plane = {"adhesion","angle",
            "attraction","attractionType","collideSet","collideWater",
            "color","colorHSVA","drawBorder","edgeBlur","friction","glued",
            "heteroCollide","immortal","Killer","materialVelocity",
            "materialName","onClick","onCollide","onDie","onHitByLaser","onKey","onSpawn",
            "opaqueBorders","pos","postStep","reflectiveness","refractiveIndex",
            "resources","restitution","texture","textureClamped","textureMatrix","timetolive","update",
            "velocityDamping","zDepth"
        };

        public static readonly string[] laser = {"collideSet","collideWater",
            "color","colorHSVA","cutter","fadeDist","followGeometry","legacyMode",
            "maxCuts","maxRays","onClick","onDie","onKey","onLaserHit","onSpawn",
            "opaqueBorders","pos","postStep","resources","rotation","showLaserBodyAttrib",
            "size","timetolive","update","velocity","zDepth"
        };

        public static readonly string[] pen = {"color","colorHSVA","fadeTime",
           "onClick","onDie","onKey","onSpawn",
            "opaqueBorders","pos","postStep",
            "resources","rotation","size","timetolive",
            "update","zDepth"
        };

        public static readonly string[] hinge = {"allowDirectSolve","autoBend",
            "autoBrake","bend","bendConstant","bendTarget","ccw","color",
            "colorHSVA","distanceLimit","forceDirectSolve","hingeConstant",
            "ImpulseLimit","legacyMode","motor","motorSpeed","motorTorque",
            "onClick","onDie","onKey","onSpawn","opaqueBorders","postStep",
            "resources","size","timetolive","update","zDepth"
        };

        public static readonly string[] fixjoint = {"color","colorHSVA","onClick",
            "onDie","onKey","onSpawn","opaqueBorders","postStep","resources",
            "size","timetolive","update","zDepth"
        };

        public static readonly string[] thruster = {"color","colorHSVA","followGeometry",
            "force","onClick","onDie","onKey","onSpawn","opaqueBorders","pos",
            "postStep","resources","rotation","size","timetolive","update","zDepth"
        };

        public static readonly string[] water = { "vecs", "version" };
    }
}