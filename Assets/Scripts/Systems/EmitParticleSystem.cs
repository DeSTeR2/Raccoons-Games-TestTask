using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public enum ParticleType
{
    BlockMerge
}

[Serializable]
public class Particle
{
    public delegate void Callback(Particle particle);

    public ParticleType particleType;
    public List<ParticleSystem> particles;

    public Particle(ParticleType particleType, List<ParticleSystem> particles)
    {
        this.particleType = particleType;
        this.particles = particles;
    }

    public async void Play(Vector3 position, Callback callback)
    {
        float maxDuration = 0;

        foreach (var p in particles)
        {
            p.gameObject.transform.position = position;
            p.Play();

            maxDuration = MathF.Max(maxDuration, p.main.duration);
        }

        await Task.Delay((int)(maxDuration * 1000));
        callback(this);
    }
}

public class EmitParticleSystem : MonoBehaviour
{
    public static EmitParticleSystem Instance;
    [SerializeField] private List<Particle> particles;

    private readonly Dictionary<ParticleType, ObjectPool<Particle>> _objectPool = new();
    private readonly Dictionary<ParticleType, Particle> _particlesDictionary = new();

    private ParticleType _currentParticleType;

    private void Awake()
    {
        Instance = this;

        for (var i = 0; i < particles.Count; i++)
        {
            var pool = new ObjectPool<Particle>(AddParticle);

            _objectPool.Add(particles[i].particleType, pool);
            _particlesDictionary.Add(particles[i].particleType, particles[i]);
        }
    }

    public void Play(ParticleType particleType, Vector3 position)
    {
        if (_objectPool.ContainsKey(_currentParticleType) == false)
        {
            Debug.LogWarning($"There is no particle with type {particleType}");
            return;
        }

        _currentParticleType = particleType;
        var particle = _objectPool[_currentParticleType].Get();

        particle.Play(position, BackParticle);
    }

    private void BackParticle(Particle particle)
    {
        _objectPool[particle.particleType].Release(particle);
    }

    private Particle AddParticle()
    {
        var list = new List<ParticleSystem>();
        for (var i = 0; i < _particlesDictionary[_currentParticleType].particles.Count; i++)
        {
            var p = Instantiate(_particlesDictionary[_currentParticleType].particles[i]);
            list.Add(p);
        }

        var particle = new Particle(_currentParticleType, list);
        return particle;
    }
}