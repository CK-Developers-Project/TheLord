using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;
using Developers.Util;

[RequireComponent(typeof(AudioSource)), RequireComponent(typeof(GameAudio))]
public class SoundManager : MonoSingleton<SoundManager>
{
    public GameObject audio_prefab;

    public GameAudio main_audio;
    public AudioMixer mixer;

    public AudioMixerGroup master_group;
    public AudioMixerGroup sfx_group;
    public AudioMixerGroup music_group;

    [Range(0f, 1f)]
    public float sfx_volum = 1f;
    [Range(0f, 1f)]
    public float music_volum = 1f;

    [Range(0f, 1f)]
    public float s_volum = 1f;
    [Range(0f, 1f)]
    public float m_volum = 1f;

    public bool sfx_mute = false;
    public bool music_mute = false;

    [SerializeField, Range(1, 10)]
    private int sfx_index = 5;
    private List<GameAudio> sfx_audio_list = new List<GameAudio>();
    private int next_index = 0;

    private enum MixerParameter
    {
        SFX_Vol,
        Music_Vol,
    }
    private Dictionary<MixerParameter, string> mixer_para = new Dictionary<MixerParameter, string>() {
            {MixerParameter.SFX_Vol, "SFX_Vol" },
            {MixerParameter.Music_Vol, "Music_Vol" },
        };

    [HideInInspector]
    public bool is_running = false;

    public void set_volume()
    {
        transform.GetComponent<AudioSource>().volume = m_volum;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<AudioSource>().volume = s_volum;
        }
    }
    public void set_sfx_volum(float volum)
    {
        s_volum = volum;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<AudioSource>().volume = volum;
        }
        /*if(volum > 1f) {
            volum = 1f;
        } else if(volum < 0f) {
            volum = 0f;
        }
        sfx_volum = volum;
        float result = -80 + (sfx_volum * 80f);
        if ( !sfx_mute ) {
            mixer.SetFloat ( mixer_para[MixerParameter.SFX_Vol], result );
        }*/
    }

    public void set_music_volum(float volum)
    {

        transform.GetComponent<AudioSource>().volume = volum;
        m_volum = volum;

        /*if ( volum > 1f ) {
            volum = 1f;
        } else if ( volum < 0f ) {
            volum = 0f;
        }
        music_volum = volum;
        float result = -80 + (music_volum * 80f);
        if ( !music_mute ) {
            mixer.SetFloat ( mixer_para[MixerParameter.Music_Vol], result );
        }*/
    }

    public void set_sfx_mute(bool value)
    {
        sfx_mute = value;
        if (value)
        {
            mixer.SetFloat(mixer_para[MixerParameter.SFX_Vol], -80f);
        }
        else
        {
            float result = -80 + (sfx_volum * 80f);
            mixer.SetFloat(mixer_para[MixerParameter.SFX_Vol], result);
        }
    }

    public void set_music_mute(bool value)
    {
        music_mute = value;
        if (value)
        {
            mixer.SetFloat(mixer_para[MixerParameter.Music_Vol], -80f);
        }
        else
        {
            float result = -80 + (music_volum * 80f);
            mixer.SetFloat(mixer_para[MixerParameter.Music_Vol], result);
        }
    }


    public void play(AudioClip clip, double tick_time, double attack_time, double release_time, int midi_note_number = 60)
    {
        float pitch = midi_note_to_pitch(midi_note_number, Midi_Note_C4);

        int cnt = 0;
        while (sfx_audio_list[next_index].state != ASREnvelope.State.Idle)
        {
            if (cnt >= sfx_index)
            {
                GameObject audio_obj = Instantiate(audio_prefab, transform);
                GameAudio game_audio = audio_obj.GetComponent<GameAudio>();
                audio_obj.name = "Sfx Game Audio - Destroy";
                game_audio.play(clip, pitch, tick_time, attack_time, release_time, true);
                next_index = (next_index + 1) % sfx_audio_list.Count;
                return;
            }
            cnt++;
            next_index = (next_index + 1) % sfx_audio_list.Count;
        }

        sfx_audio_list[next_index].play(clip, pitch, tick_time, attack_time, release_time);
        next_index = (next_index + 1) % sfx_audio_list.Count;
    }


    public void set_music(AudioClip clip)
    {
        main_audio.source.clip = clip;
    }


    public void on_music()
    {
        StartCoroutine(Emusic_on());
    }

    public void on_music(AudioClip clip)
    {
        set_music(clip);
        StartCoroutine(Emusic_on());
    }


    public void off_music()
    {
        StartCoroutine(Emusic_off());
    }


    private void initialize()
    {
        set_sfx_volum(sfx_volum);
        set_music_volum(music_volum);

        set_sfx_mute(sfx_mute);
        set_music_mute(music_mute);
    }


    const int Midi_Note_C4 = 60;
    private float midi_note_to_pitch(int midi_note, int base_note)
    {
        int semitone_offset = midi_note - base_note;
        return (float)System.Math.Pow(2.0, semitone_offset / 12.0);
    }


    private IEnumerator Emusic_erasure(float volum)
    {
        while (main_audio.source.volume != volum)
        {
            main_audio.source.volume = Mathf.MoveTowards(main_audio.source.volume, volum, Time.deltaTime);
            yield return null;
        }
    }


    private IEnumerator Emusic_on()
    {
        if (is_running)
        {
            yield break;
        }
        is_running = true;
        main_audio.source.volume = m_volum;
        main_audio.play(AudioSettings.dspTime + Time.deltaTime, 0f, 1f);
        //yield return StartCoroutine ( Emusic_erasure ( 1f ) );
        StartCoroutine(Emusic_play());
    }


    private IEnumerator Emusic_off()
    {
        if (!is_running)
        {
            yield break;
        }
        is_running = false;
        //yield return StartCoroutine ( Emusic_erasure ( 0f ) );
        main_audio.source.Stop();
    }


    private IEnumerator Emusic_play()
    {
        while (is_running)
        {
            if (main_audio.state == ASREnvelope.State.Idle)
            {
                main_audio.play(AudioSettings.dspTime + Time.deltaTime, 0f, 1f);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    ////////////////////////////////////////////////////////////////////////////
    ///                               Unity                                  ///
    ////////////////////////////////////////////////////////////////////////////

    protected override void Awake()
    {
        base.Awake();

        if (!main_audio)
        {
            main_audio = GetComponent<GameAudio>();
        }

        if (!main_audio.source)
        {
            main_audio.source = GetComponent<AudioSource>();
        }

        if (!mixer)
        {
            mixer = Resources.Load<AudioMixer>("Audio/AudioMixer");
        }

        if (!master_group)
        {
            master_group = mixer.FindMatchingGroups("Master")[0];
        }

        if (!sfx_group)
        {
            sfx_group = mixer.FindMatchingGroups("SFX")[0];
        }

        if (!music_group)
        {
            music_group = mixer.FindMatchingGroups("Music")[0];
        }

        if (!main_audio.source.outputAudioMixerGroup)
        {
            main_audio.source.outputAudioMixerGroup = music_group;
        }


        if (sfx_audio_list.Count >= sfx_index)
        {
            return;
        }
        else
        {
            while (sfx_audio_list.Count < sfx_index)
            {
                GameObject audio_obj = Instantiate(audio_prefab, transform);
                sfx_audio_list.Add(audio_obj.GetComponent<GameAudio>());
                audio_obj.name = "Sfx Game Audio - " + sfx_audio_list.Count;
            }
        }

        set_volume();
    }

    private void Start()
    {
        initialize();
    }


    private void OnValidate()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            set_sfx_volum(sfx_volum);
            set_music_volum(music_volum);

            set_sfx_mute(sfx_mute);
            set_music_mute(music_mute);
        }
#endif
    }


    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}