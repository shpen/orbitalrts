using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlePhysics : MonoBehaviour
{
	ParticleSystem m_System;
	ParticleSystem.Particle[] m_Particles;
	public float m_Drift = 0.01f;

	private void FixedUpdate()
	{
		InitializeIfNeeded();

		// GetParticles is allocation free because we reuse the m_Particles buffer between updates
		int numParticlesAlive = m_System.GetParticles(m_Particles);

		// Change only the particles that are alive
		for (int i = 0; i < numParticlesAlive; i++) {
//			Vector3 force = Attractor.GetTotalAttractionForce(m_Particles[i].position, 1);
//			m_Particles[i].velocity += force * Time.fixedDeltaTime;//Vector3.up * m_Drift;


			float scaledTime = Time.deltaTime;// * 2f;

			Vector3 force = Vector3.zero;
			foreach (Attractor a in Attractor.Attractors) {
				Rigidbody arb = a.GetRigidbody();
				force -= GetAttractionForce(m_Particles[i].position, 1f, arb.position, arb.mass);
			}
			Vector3 newVelocity = m_Particles[i].velocity + force * scaledTime;
			m_Particles[i].position += (m_Particles[i].velocity + newVelocity) / 2 * scaledTime;
			m_Particles[i].velocity = newVelocity;
		}

		// Apply the particle changes to the particle system
		m_System.SetParticles(m_Particles, numParticlesAlive);
	}

	void InitializeIfNeeded()
	{
		if (m_System == null)
			m_System = GetComponent<ParticleSystem>();

		if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
			m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
	}

	private Vector3 GetAttractionForce(Vector3 mypos, float mymass, Vector3 position, float mass) {
		Vector3 direction = mypos - position;
		float distanceSqr = direction.sqrMagnitude;

		float forceMag = 1f * mymass * mass / distanceSqr;
		return direction.normalized * forceMag;
	}
}