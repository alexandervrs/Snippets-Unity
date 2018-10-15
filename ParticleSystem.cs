
/**
 * ParticleSystem.cs
 * ParticleSystem related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Setup Particle System
----------------------------------------- */

// place attribute on top of class, include a ParticleSystem component
[RequireComponent(typeof(ParticleSystem))]

// retrieve the attached particle system renderer
ParticleSystem PSys;
PSys = this.gameObject.GetComponent<ParticleSystem>();
ParticleSystemRenderer PSysRenderer = PSys.GetComponent<ParticleSystemRenderer>();

// retrieve the particle system modules to access functionality
ParticleSystem.MainModule      			   PSysSettings    = PSys.main;
ParticleSystem.EmissionModule  			   PSysEmission    = PSys.emission;
ParticleSystem.TrailModule                 PSysTrails      = PSys.trails;
ParticleSystem.ColorOverLifetimeModule     PSysColorOL     = PSys.colorOverLifetime;
ParticleSystem.VelocityOverLifetimeModule  PSysVelocityOL  = PSys.velocityOverLifetime;
ParticleSystem.TextureSheetAnimationModule PSysTextureAnim = PSys.textureSheetAnimation;
ParticleSystem.TriggerModule               PSysTrigger     = PSys.trigger;

/* render settings */
PSysRenderer.material       = Resources.Load("StarGlint", typeof(Material)) as Material;
PSysRenderer.trailMaterial  = Resources.Load("StarTrail", typeof(Material)) as Material;
PSysRenderer.sortingLayerID = SortingLayer.NameToID("particles");

/* adjust settings */
PSysSettings.maxParticles              = 1000;
PSysSettings.playOnAwake               = false;
PSysSettings.prewarm                   = false;
PSysSettings.startSpeed                = new ParticleSystem.MinMaxCurve(5.0f, 10.0f);
PSysSettings.gravityModifier           = 0.3f;
PSysSettings.gravityModifierMultiplier = 1.0f;
PSysSettings.startDelay                = 0.0f;
PSys.Stop(); // PS needs to be stopped before adjusting lifetime and duration
PSysSettings.startLifetime             = 1.0f;
PSysSettings.duration                  = 1.0f;
PSysSettings.loop                      = false;
PSysSettings.simulationSpace = ParticleSystemSimulationSpace.Local; // or World so that particles move independently from the gameObject

/* velocity over lifetime module settings */
PSysVelocityOL.enabled       = true;
PSysVelocityOL.speedModifier = 1.0f;

/* color over lifetime module settings */
Gradient g; // create a new gradient
GradientColorKey[] gck;
GradientAlphaKey[] gak;
g = new Gradient();
gck = new GradientColorKey[2];
gck[0].color = Color.red;
gck[0].time = 0.0F;
gck[1].color = Color.blue;
gck[1].time = 1.0F;
gak = new GradientAlphaKey[2];
gak[0].alpha = 1.0F;
gak[0].time = 0.0F;
gak[1].alpha = 0.0F;
gak[1].time = 1.0F;
g.SetKeys(gck, gak);

PSysColorOL.enabled = true;
PSysColorOL.color   = g; // attach gradient to particle

/* trail module settings */
PSysTrails.enabled           = true;
PSysTrails.ratio             = 0.2f;
PSysTrails.dieWithParticles  = false;
PSysTrails.lifetime          = 1.0f;
PSysTrails.widthOverTrail    = 0.9f;
PSysTrails.minVertexDistance = 0.3f;
PSysTrails.colorOverLifetime = g; // attach gradient to particle trail

/* emission module settings */
PSysEmission.enabled          = true;
PSysEmission.rateOverTime     = 0.0f; // set to non-zero for constant spawning
PSysEmission.rateOverDistance = 0.0f;

// set bursts instead of rate over time
PSysEmission.SetBursts( // set custom burst intervals
	new ParticleSystem.Burst [] {
		new ParticleSystem.Burst(1.0f, 10),
		new ParticleSystem.Burst(2.0f, 30)
	}
);

/* texturesheet animation module settings */
// (using a 3x3 animation grid)
AnimationCurve curv = new AnimationCurve(); // create a new standard minmax curve for frame playback
curv.AddKey(0.0f, 0.0f);
curv.AddKey(1.0f, 1.0f);

PSysTextureAnim.enabled       = true;
PSysTextureAnim.mode          = ParticleSystemAnimationMode.Grid;
PSysTextureAnim.numTilesX     = 3; // how many rows/images in the grid
PSysTextureAnim.numTilesY     = 3; // how many columns/images in the grid
PSysTextureAnim.animation     = ParticleSystemAnimationType.WholeSheet;
PSysTextureAnim.frameOverTime = new ParticleSystem.MinMaxCurve(1.0f, curv); // float multiplier, AnimationCurve min, AnimationCurve max
PSysTextureAnim.cycleCount    = 1; // how many times to loop animation
PSysTextureAnim.flipU         = 0;
PSysTextureAnim.flipV         = 0;
PSysTextureAnim.startFrame    = 0;
PSysTextureAnim.useRandomRow  = false;

/* trigger module settings */
GameObject pTrigger = new GameObject();
pTrigger.name = "ParticleSysTrigger";
pTrigger.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-3f, gameObject.transform.position.z);
pTrigger.transform.SetParent(gameObject.transform, true); // create and parent a Box Collider to the particle system

BoxCollider PSysCollider = pTrigger.gameObject.AddComponent<BoxCollider>();
PSysCollider.size = new Vector3(5f, 0.1f, 0f);

PSysTrigger.enabled = true;
PSysTrigger.inside  = ParticleSystemOverlapAction.Kill;
PSysTrigger.enter   = ParticleSystemOverlapAction.Kill;
PSysTrigger.outside = ParticleSystemOverlapAction.Ignore;
PSysTrigger.exit    = ParticleSystemOverlapAction.Ignore;
PSysTrigger.SetCollider(0, PSysCollider); // index, collider


/* -----------------------------------------
   Playback Particle System
----------------------------------------- */

// pre-run particle system
PSys.Simulate(3.0f); //pre-run for 3 seconds

// burst particles
PSys.Emit(100);

// stream particles
PSys.Play(); // PSys.Stop() to stop emission

// clear the particle system
PSys.Clear();

