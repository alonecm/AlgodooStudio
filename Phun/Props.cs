namespace AlgodooStudio.Phun
{
    /// <summary>
    /// 实体的基本属性值
    /// </summary>
    internal static class Props
    {
        internal static string[] circle = {"adhesion","airFrictionMult","angle",
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

        internal static string[] box = {"adhesion","airFrictionMult","angle",
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

        internal static string[] polygon = {"adhesion","airFrictionMult","angle",
            "angvel","attraction","attractionType","collideSet","collideWater",
            "color","colorHSVA","controllerAcc","controllerInvertX","controllerInvertY",
            "controllerReverseXY","density","drawBorder","edgeBlur","forceVertexDrawing","friction","glued",
            "heteroCollide","immortal","inertiaMultiplier","Killer","materialVelocity",
            "materialName","onClick","onCollide","onDie","onHitByLaser","onKey","onSpawn",
            "opaqueBorders","pos","postStep","reflectiveness","refractiveIndex",
            "resources","restitution","showForceArrows","showMomentum",
            "showVelocity","texture","textureClamped","textureMatrix","timetolive","update",
            "vel","velocityDamping","zDepth"
        };

        internal static string[] plane = {"adhesion","angle",
            "attraction","attractionType","collideSet","collideWater",
            "color","colorHSVA","drawBorder","edgeBlur","friction","glued",
            "heteroCollide","immortal","Killer","materialVelocity",
            "materialName","onClick","onCollide","onDie","onHitByLaser","onKey","onSpawn",
            "opaqueBorders","pos","postStep","reflectiveness","refractiveIndex",
            "resources","restitution","texture","textureClamped","textureMatrix","timetolive","update",
            "velocityDamping","zDepth"
        };

        internal static string[] laser = {"collideSet","collideWater",
            "color","colorHSVA","cutter","fadeDist","followGeometry","legacyMode",
            "maxCuts","maxRays","onClick","onDie","onKey","onLaserHit","onSpawn",
            "opaqueBorders","pos","postStep","resources","Rotation","showLaserBodyAttrib",
            "size","timetolive","update","velocity","zDepth"
        };

        internal static string[] pen = {"color","colorHSVA","fadeTime",
           "onClick","onDie","onKey","onSpawn",
            "opaqueBorders","pos","postStep",
            "resources","Rotation","size","timetolive",
            "update","zDepth"
        };

        internal static string[] hinge = {"allowDirectSolve","autoBend",
            "autoBrake","bend","bendConstant","bendTarget","ccw","color",
            "colorHSVA","distanceLimit","forceDirectSolve","hingeConstant",
            "ImpulseLimit","legacyMode","motor","motorSpeed","motorTorque",
            "onClick","onDie","onKey","onSpawn","opaqueBorders","postStep",
            "resources","size","timetolive","update","zDepth"
        };

        internal static string[] fixjoint = {"color","colorHSVA","onClick",
            "onDie","onKey","onSpawn","opaqueBorders","postStep","resources",
            "size","timetolive","update","zDepth"
        };

        internal static string[] thruster = {"color","colorHSVA","followGeometry",
            "force","onClick","onDie","onKey","onSpawn","opaqueBorders","pos",
            "postStep","resources","Rotation","size","timetolive","update","zDepth"
        };

        internal static string[] water = { "vecs", "version" };
    }
}