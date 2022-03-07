using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("SoundManager");
                obj.AddComponent<SoundManager>();
                _instance = obj.GetComponent<SoundManager>();
                _instance._sourceQueue = new Queue<AudioSource>();
                _instance._soundFilePath = string.Concat(Application.persistentDataPath, "/sound_file.txt");
                if (File.Exists(_instance._soundFilePath))
                {
                    _instance._baseVolume = float.Parse(File.ReadAllText(_instance._soundFilePath));
                }
                else
                {
                    _instance.BaseVolume = 0.5f;
                }
                DontDestroyOnLoad(obj);
            }

            return _instance;
        }
    }

    public Queue<AudioSource> _sourceQueue;
    private float _baseVolume;
    public float BaseVolume
    {
        get { return _baseVolume; }
        set
        {
            _baseVolume = value;
            File.WriteAllText(_soundFilePath, _baseVolume.ToString());
        }
    }

    public string _soundFilePath;

    public AudioSource PlaySound(AudioClip clip, float volume, bool loop)
    {
        AudioSource source = null;

        if (_sourceQueue.Count == 0)
        {
            GameObject obj = new GameObject("AudioSource");
            obj.transform.SetParent(transform);
            obj.AddComponent<AudioSource>();
            source = obj.GetComponent<AudioSource>();
            source.playOnAwake = false;
        }
        else
        {
            source = _sourceQueue.Peek();
            if (source.isPlaying)
            {
                GameObject obj = new GameObject("AudioSource");
                obj.transform.SetParent(transform);
                obj.AddComponent<AudioSource>();
                source = obj.GetComponent<AudioSource>();
                source.playOnAwake = false;
            }
            else
            {
                _sourceQueue.Dequeue();
            }
        }

        source.clip = clip;
        source.volume = volume;
        source.loop = loop;

        source.gameObject.SetActive(true);
        _sourceQueue.Enqueue(source);

        return source;
    }
}