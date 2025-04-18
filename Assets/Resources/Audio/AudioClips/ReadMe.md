# MyAudioManager

## Overview
`MyAudioManager` is a singleton-based audio management system for Unity that handles volume control, muting, and playback of different types of audio clips, including SFX, guidance, and environment sounds. It utilizes Unity's `AudioMixer` for volume adjustments and stores settings using `PlayerPrefs`.

## Features
- **Singleton Pattern:** Ensures only one instance of `MyAudioManager` exists.
- **Volume Control:** Adjustable master, SFX, guidance, and environment volume using sliders.
- **Mute Functionality:** Toggle mute states for each audio type with UI buttons.
- **Persistent Settings:** Stores volume and mute settings using `PlayerPrefs`.
- **Audio Playback:** Supports one-shot and looped playback of audio clips from `Resources`.
- **Language-Specific Audio:** Plays guidance audio in different languages based on `PlayerPrefs` or provided parameters.

## Setup Instructions

### 1. Attach to GameObject
- Attach `MyAudioManager` to an empty GameObject in your scene.
- Ensure the GameObject has an `AudioSource` component for each audio category:
  - `SfxSource`
  - `GuidanceSource`
  - `EnvironmentSource`

### 2. Assign UI Elements
- Assign `Slider` components for:
  - `masterSlider`
  - `sfxSlider`
  - `guidanceSlider`
  - `environmentSlider`
- Assign `Button` components for muting audio:
  - `masterButton`
  - `sfxButton`
  - `guidanceButton`
  - `environmentButton`
- Assign `muted` and `unmuted` sprites for toggle states.

### 3. Connect to AudioMixer
- Assign a Unity `AudioMixer` to `masterMixer`.
- Ensure it has exposed parameters named:
  - `Master`
  - `sfx`
  - `dia`
  - `env`

## Usage

### Volume Control
- **Automatic Handling:** Sliders automatically adjust volume and save preferences.
- **Manual Control:** Call:
  ```csharp
  MyAudioManager.instance.SetMasterVolume(0.5f);
  MyAudioManager.instance.SetSfxVolume(1f);
  ```

### Mute/Unmute
- Click mute buttons in UI or toggle programmatically:
  ```csharp
  MyAudioManager.instance.MasterButtonClicked();
  ```

### Playing Sounds
- **SFX:**
  ```csharp
  MyAudioManager.instance.PlaySfxOneShot("jump");
  ```
- **Guidance (Default Language):**
  ```csharp
  MyAudioManager.instance.PlayGuidance("welcome");
  ```
- **Guidance (Specific Language):**
  ```csharp
  MyAudioManager.instance.PlayGuidance("welcome", Language.Spanish);
  ```
- **Guidance (Level-Specific):**
  ```csharp
  MyAudioManager.instance.PlayGuidance("intro", "Level1");
  ```
- **Environment Sounds:**
  ```csharp
  MyAudioManager.instance.PlayEnvironment("rain");
  ```

## Notes
- Audio clips must be inside `Resources/Audio/AudioClips/GeneratedAudio/`.
- `PlayerPrefs` stores volume/mute settings persistently.
- Make sure the correct language folder exists inside the `Miscelaneous` directory for guidance clips.