using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Sistema di configurazione per ragdoll dei personaggi.
/// Definisce la struttura scheletrica per ogni razza, con ossa personalizzate.
/// </summary>
public class RagdollConfiguration : MonoBehaviour
{
    [System.Serializable]
    public class BoneSetup
    {
        public string BoneName;
        public HumanBodyBones HumanBodyBone;
        public Vector3 LocalPosition;
        public Vector3 LocalRotation;
        public Vector3 ColliderSize;
        public float Mass;
        public bool IsRequired;
        public string ParentBoneName;
    }

    [System.Serializable]
    public class RagdollSetup
    {
        public RaceType Race;
        public List<BoneSetup> Bones;
        public float TotalMass;
        public bool HasCustomSkull; // Per Goblin, Orc, Ogre con caratteristiche speciali
        public List<string> CustomBones; // Ossa speciali (corna, denti a sciabola, etc.)
    }

    /// <summary>
    /// Definisce la struttura scheletrica standard umanoide.
    /// </summary>
    public static List<BoneSetup> GetStandardHumanoidSkeleton()
    {
        return new List<BoneSetup>
        {
            // TESTA E COLLO
            new BoneSetup { BoneName = "Head", HumanBodyBone = HumanBodyBones.Head, Mass = 4f, IsRequired = true, ParentBoneName = "Neck" },
            new BoneSetup { BoneName = "Neck", HumanBodyBone = HumanBodyBones.Neck, Mass = 2f, IsRequired = true, ParentBoneName = "UpperChest" },
            
            // TORSO
            new BoneSetup { BoneName = "UpperChest", HumanBodyBone = HumanBodyBones.UpperChest, Mass = 15f, IsRequired = true, ParentBoneName = "Chest" },
            new BoneSetup { BoneName = "Chest", HumanBodyBone = HumanBodyBones.Chest, Mass = 20f, IsRequired = true, ParentBoneName = "Spine" },
            new BoneSetup { BoneName = "Spine", HumanBodyBone = HumanBodyBones.Spine, Mass = 15f, IsRequired = true, ParentBoneName = "Hips" },
            new BoneSetup { BoneName = "Hips", HumanBodyBone = HumanBodyBones.Hips, Mass = 15f, IsRequired = true, ParentBoneName = null },
            
            // BRACCIA SINISTRE
            new BoneSetup { BoneName = "LeftShoulder", HumanBodyBone = HumanBodyBones.LeftShoulder, Mass = 1f, IsRequired = true, ParentBoneName = "UpperChest" },
            new BoneSetup { BoneName = "LeftUpperArm", HumanBodyBone = HumanBodyBones.LeftUpperArm, Mass = 2f, IsRequired = true, ParentBoneName = "LeftShoulder" },
            new BoneSetup { BoneName = "LeftLowerArm", HumanBodyBone = HumanBodyBones.LeftLowerArm, Mass = 1.5f, IsRequired = true, ParentBoneName = "LeftUpperArm" },
            new BoneSetup { BoneName = "LeftHand", HumanBodyBone = HumanBodyBones.LeftHand, Mass = 0.5f, IsRequired = true, ParentBoneName = "LeftLowerArm" },
            
            // BRACCIA DESTRE
            new BoneSetup { BoneName = "RightShoulder", HumanBodyBone = HumanBodyBones.RightShoulder, Mass = 1f, IsRequired = true, ParentBoneName = "UpperChest" },
            new BoneSetup { BoneName = "RightUpperArm", HumanBodyBone = HumanBodyBones.RightUpperArm, Mass = 2f, IsRequired = true, ParentBoneName = "RightShoulder" },
            new BoneSetup { BoneName = "RightLowerArm", HumanBodyBone = HumanBodyBones.RightLowerArm, Mass = 1.5f, IsRequired = true, ParentBoneName = "RightUpperArm" },
            new BoneSetup { BoneName = "RightHand", HumanBodyBone = HumanBodyBones.RightHand, Mass = 0.5f, IsRequired = true, ParentBoneName = "RightLowerArm" },
            
            // GAMBE SINISTRE
            new BoneSetup { BoneName = "LeftUpperLeg", HumanBodyBone = HumanBodyBones.LeftUpperLeg, Mass = 8f, IsRequired = true, ParentBoneName = "Hips" },
            new BoneSetup { BoneName = "LeftLowerLeg", HumanBodyBone = HumanBodyBones.LeftLowerLeg, Mass = 4f, IsRequired = true, ParentBoneName = "LeftUpperLeg" },
            new BoneSetup { BoneName = "LeftFoot", HumanBodyBone = HumanBodyBones.LeftFoot, Mass = 1f, IsRequired = true, ParentBoneName = "LeftLowerLeg" },
            new BoneSetup { BoneName = "LeftToes", HumanBodyBone = HumanBodyBones.LeftToes, Mass = 0.2f, IsRequired = false, ParentBoneName = "LeftFoot" },
            
            // GAMBE DESTRE
            new BoneSetup { BoneName = "RightUpperLeg", HumanBodyBone = HumanBodyBones.RightUpperLeg, Mass = 8f, IsRequired = true, ParentBoneName = "Hips" },
            new BoneSetup { BoneName = "RightLowerLeg", HumanBodyBone = HumanBodyBones.RightLowerLeg, Mass = 4f, IsRequired = true, ParentBoneName = "RightUpperLeg" },
            new BoneSetup { BoneName = "RightFoot", HumanBodyBone = HumanBodyBones.RightFoot, Mass = 1f, IsRequired = true, ParentBoneName = "RightLowerLeg" },
            new BoneSetup { BoneName = "RightToes", HumanBodyBone = HumanBodyBones.RightToes, Mass = 0.2f, IsRequired = false, ParentBoneName = "RightFoot" }
        };
    }

    /// <summary>
    /// Ottiene la configurazione ragdoll specifica per una razza.
    /// </summary>
    public static RagdollSetup GetRagdollSetupForRace(RaceType race)
    {
        var setup = new RagdollSetup
        {
            Race = race,
            Bones = new List<BoneSetup>(GetStandardHumanoidSkeleton()),
            CustomBones = new List<string>()
        };

        // Personalizzazioni per razza
        switch (race)
        {
            case RaceType.ElfNature:
                setup.TotalMass = 50f; // Magri, leggeri
                setup.HasCustomSkull = false;
                // Orecchie a punta (da aggiungere in Blender)
                setup.CustomBones.Add("LeftEarPoint");
                setup.CustomBones.Add("RightEarPoint");
                break;

            case RaceType.Goblin:
                setup.TotalMass = 40f; // Piccoli e magri
                setup.HasCustomSkull = true; // Teschio modificato
                // Naso lungo e orecchie lunghe
                setup.CustomBones.Add("NoseBone");
                setup.CustomBones.Add("LeftLongEar");
                setup.CustomBones.Add("RightLongEar");
                break;

            case RaceType.Orc:
                setup.TotalMass = 120f; // Molto muscolosi e pesanti
                setup.HasCustomSkull = true; // Teschio con corna
                // Corna e denti a sciabola
                setup.CustomBones.Add("LeftHorn");
                setup.CustomBones.Add("RightHorn");
                setup.CustomBones.Add("LeftSaberTooth");
                setup.CustomBones.Add("RightSaberTooth");
                // Aumenta massa per muscoli
                foreach (var bone in setup.Bones)
                {
                    bone.Mass *= 1.5f;
                }
                break;

            case RaceType.WhiteElf:
                setup.TotalMass = 65f; // Magri e tonificati
                setup.HasCustomSkull = false;
                // Orecchie a punta
                setup.CustomBones.Add("LeftEarPoint");
                setup.CustomBones.Add("RightEarPoint");
                break;

            case RaceType.DarkElf:
                setup.TotalMass = 60f; // Magri
                setup.HasCustomSkull = false;
                // Orecchie a punta
                setup.CustomBones.Add("LeftEarPoint");
                setup.CustomBones.Add("RightEarPoint");
                break;

            case RaceType.Dwarf:
                setup.TotalMass = 70f; // Robusti e compatti
                setup.HasCustomSkull = false;
                // Riduci dimensioni ossa per corporatura bassa
                foreach (var bone in setup.Bones)
                {
                    bone.Mass *= 0.8f;
                }
                break;

            case RaceType.Human:
                setup.TotalMass = 75f; // Media
                setup.HasCustomSkull = false;
                // Scheletro standard
                break;

            case RaceType.Ogre:
                setup.TotalMass = 250f; // Massicci
                setup.HasCustomSkull = true; // Denti a sciabola
                // Denti a sciabola
                setup.CustomBones.Add("LeftSaberTooth");
                setup.CustomBones.Add("RightSaberTooth");
                // Aumenta massa enormemente
                foreach (var bone in setup.Bones)
                {
                    bone.Mass *= 2.5f;
                }
                break;

            case RaceType.Elf:
                setup.TotalMass = 60f;
                setup.HasCustomSkull = false;
                setup.CustomBones.Add("LeftEarPoint");
                setup.CustomBones.Add("RightEarPoint");
                break;
        }

        return setup;
    }

    /// <summary>
    /// Applica la configurazione ragdoll a un GameObject.
    /// Nota: Richiede che il GameObject abbia già una gerarchia di ossa configurata.
    /// </summary>
    public static void ApplyRagdollToGameObject(GameObject character, RaceType race)
    {
        if (character == null)
        {
            Debug.LogError("ApplyRagdollToGameObject: character è null");
            return;
        }

        var setup = GetRagdollSetupForRace(race);
        
        Debug.Log($"Applicazione ragdoll per razza {race} con {setup.Bones.Count} ossa e massa totale {setup.TotalMass}kg");

        // Questo metodo presume che le ossa siano già presenti nella gerarchia
        // In Unity, dovrai configurare manualmente il Ragdoll Wizard con questi parametri
        
        foreach (var boneSetup in setup.Bones)
        {
            // Cerca l'osso nella gerarchia
            Transform boneTransform = FindBoneInHierarchy(character.transform, boneSetup.BoneName);
            
            if (boneTransform != null)
            {
                // Aggiungi Rigidbody
                var rb = boneTransform.gameObject.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = boneTransform.gameObject.AddComponent<Rigidbody>();
                }
                rb.mass = boneSetup.Mass;
                rb.drag = 0.05f;
                rb.angularDrag = 0.05f;
                
                // Aggiungi Collider (CapsuleCollider di default)
                var collider = boneTransform.gameObject.GetComponent<CapsuleCollider>();
                if (collider == null)
                {
                    collider = boneTransform.gameObject.AddComponent<CapsuleCollider>();
                }
                
                // Aggiungi CharacterJoint (eccetto per Hips che è il root)
                if (boneSetup.ParentBoneName != null)
                {
                    var joint = boneTransform.gameObject.GetComponent<CharacterJoint>();
                    if (joint == null)
                    {
                        joint = boneTransform.gameObject.AddComponent<CharacterJoint>();
                    }
                    
                    // Connetti al parent
                    Transform parentTransform = FindBoneInHierarchy(character.transform, boneSetup.ParentBoneName);
                    if (parentTransform != null)
                    {
                        var parentRb = parentTransform.GetComponent<Rigidbody>();
                        if (parentRb != null)
                        {
                            joint.connectedBody = parentRb;
                        }
                    }
                }
                
                Debug.Log($"Configurato osso: {boneSetup.BoneName} con massa {boneSetup.Mass}kg");
            }
            else if (boneSetup.IsRequired)
            {
                Debug.LogWarning($"Osso richiesto non trovato: {boneSetup.BoneName}");
            }
        }

        // Log delle ossa custom da creare in Blender
        if (setup.CustomBones.Count > 0)
        {
            Debug.Log($"Ossa speciali per {race} da creare in Blender/Maya: {string.Join(", ", setup.CustomBones)}");
        }
    }

    /// <summary>
    /// Cerca un osso nella gerarchia del transform.
    /// </summary>
    private static Transform FindBoneInHierarchy(Transform root, string boneName)
    {
        if (root.name == boneName)
            return root;

        foreach (Transform child in root)
        {
            Transform result = FindBoneInHierarchy(child, boneName);
            if (result != null)
                return result;
        }

        return null;
    }

    /// <summary>
    /// Genera un report delle specifiche ragdoll per tutte le razze.
    /// Utile per riferimento durante la modellazione in Blender/Maya.
    /// </summary>
    public static string GenerateRagdollSpecificationReport()
    {
        System.Text.StringBuilder report = new System.Text.StringBuilder();
        report.AppendLine("=== SPECIFICHE RAGDOLL PER TUTTE LE RAZZE ===\n");

        foreach (RaceType race in System.Enum.GetValues(typeof(RaceType)))
        {
            var setup = GetRagdollSetupForRace(race);
            var physSpecs = RacePhysicalSpecs.GetSpecsForRace(race);

            report.AppendLine($"--- {race} ({physSpecs.RaceName}) ---");
            report.AppendLine($"Altezza: {physSpecs.MinHeight}-{physSpecs.MaxHeight} cm");
            report.AppendLine($"Massa totale: {setup.TotalMass} kg");
            report.AppendLine($"Corporatura: {physSpecs.BodyType}");
            report.AppendLine($"Teschio personalizzato: {(setup.HasCustomSkull ? "SÌ" : "NO")}");
            
            if (setup.CustomBones.Count > 0)
            {
                report.AppendLine($"Ossa speciali: {string.Join(", ", setup.CustomBones)}");
            }
            
            report.AppendLine($"Caratteristiche speciali:");
            report.AppendLine($"  - Orecchie a punta: {physSpecs.HasPointedEars}");
            report.AppendLine($"  - Corna: {physSpecs.HasHorns}");
            report.AppendLine($"  - Denti a sciabola: {physSpecs.HasSaberTeeth}");
            report.AppendLine($"  - Naso lungo: {physSpecs.HasLongNose}");
            report.AppendLine($"Note: {physSpecs.SpecialFeatures}");
            report.AppendLine();
        }

        report.AppendLine("\n=== STRUTTURA SCHELETRICA STANDARD ===");
        var standardBones = GetStandardHumanoidSkeleton();
        foreach (var bone in standardBones)
        {
            string required = bone.IsRequired ? "[RICHIESTO]" : "[OPZIONALE]";
            report.AppendLine($"{required} {bone.BoneName} (Massa: {bone.Mass}kg, Parent: {bone.ParentBoneName ?? "ROOT"})");
        }

        return report.ToString();
    }
}
