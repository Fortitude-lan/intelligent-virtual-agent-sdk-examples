# Intelligent Virtual Human SDK — Unity Example Project (v3.0.0)

This is a ready-to-open Unity project that consumes the Intelligent Virtual Human SDK developed by the [human computer interaction group](https://www.inf.uni-hamburg.de/en/inst/ab/hci.html) at Hamburg University. Open it and you get a working agent scene without wiring the package up yourself.

> ### 🎉 Now tracking SDK v3.0.0
>
> This template is pinned to the **v3.0.0** release of the SDK. The headline feature is **document grounding** — an agent can answer from a corpus you supply instead of from whatever the model absorbed in pre-training. v3.0.0 also adds an in-game HUD, a setup wizard, structured logging, and a typed exception hierarchy.
>
> **No API breaks** from v2.3.x — every addition is opt-in. Full details in the [changelog](https://github.com/uhhhci/intelligent-virtual-agent-sdk/blob/v3.0.0/CHANGELOG.md).

<span style="color:red"> ***Please note that the usage of the SDK requires ethical & responsible use. Details can be found [here](https://github.com/uhhhci/intelligent-virtual-agent-sdk/blob/v3.0.0/LICENSE.md).***</span>


***For more detail on the ethical Issues of impersonation and AI fakes we refer to the following [paper](https://zenodo.org/records/15413114):*** 

Oliva, R., Wiesing, M., Gállego, J., Inami, M., Interrante, V., Lecuyer, A., McDonnell, R., Nouviale, F., Pan, X., Steinicke, F., & Slater, M. (2025). Where Extended Reality and AI May Take Us: Ethical Issues of Impersonation and AI Fakes in Social Virtual Reality (Version 1). Zenodo. 



##  Demo

Our toolkit is compatible with CC4, Microsoft-rocketbox, and DIDIMO 3D virtual humans. Due to the license restriction, we only include an example character and animations from Rocketbox characters. 


## Table of content 
- [Requirements](#requirements)
- [How this project consumes the SDK](#how-this-project-consumes-the-sdk)
- [What's new in v3.0.0](#whats-new-in-v300)
- [Main Features](#main-features)
- [Quick Start](#quick-start)
- [Grounding an agent in documents](#grounding-an-agent-in-documents)
- [Documentation](#documentation)
- [DIDIMO Character License Notice](#didimo-character-license-notice)
- [Rocketbox Character License Notice](#rocketbox-characters-license-notice)
- [Mixamo Animations](#mixamo-animations)
- [CC4 Characters and Animations](#reallusion-animation)
- [License of this toolkit](#license)
- [Citation](#citation)
- [Acknowledgement](#acknowledgement)

### Requirements
* Unity 2022.3 LTS and above, Universal Render Pipeline (URP). This template file implements unity 6.2
* **Git** must be installed and on your `PATH` — Unity's Package Manager shells out to it to fetch the SDK.
* **Git LFS** must be installed. The SDK ships character models and native audio DLLs through LFS; without it you will get unusable pointer files instead of real assets.

### How this project consumes the SDK

The SDK is referenced as a git dependency pinned to a release tag, in [`Packages/manifest.json`](./Packages/manifest.json):

```json
"de.uhh.hci.ivh.core": "https://git.informatik.uni-hamburg.de/presence/public/iva-sdk-core-public.git#v3.0.0"
```

Unity resolves this on first open and caches it under `Library/PackageCache/`. The package is read-only there — see [developing the package](https://github.com/uhhhci/intelligent-virtual-agent-sdk/blob/v3.0.0/Documentations~/howToDevelopPackage.md) if you need to edit the SDK itself.

To move to a different release, change the tag after `#`. Dropping the `#v3.0.0` suffix entirely tracks the tip of `main`, which is not recommended for reproducible work. The same release is mirrored on [GitHub](https://github.com/uhhhci/intelligent-virtual-agent-sdk) if you prefer that remote:

```json
"de.uhh.hci.ivh.core": "https://github.com/uhhhci/intelligent-virtual-agent-sdk.git#v3.0.0"
```

#### ⚠️ Windows: enable long paths first

Unity caches git packages under a deep path (`Library/PackageCache/de.uhh.hci.ivh.core@<40-char-commit-sha>/…`). Combined with the SDK's nested plugin folders this exceeds the Windows 260-character `MAX_PATH` limit, and the checkout fails part-way with `error: unable to create file … Filename too long`. The Azure Speech DLLs are the first casualties, and Unity then reports missing assemblies.

Run this **once**, before opening the project:

```sh
git config --global core.longpaths true
```

Keeping the project close to the drive root (for example `C:\dev\iva-example`) also helps, since the limit applies to the whole absolute path. macOS and Linux are unaffected.

### What's new in v3.0.0

- **Document grounding.** Three interchangeable strategies, all implementing one `IContextProvider` contract and combinable on a single agent: editor-baked `KnowledgeBase` assets with Gemini-embedding RAG, per-turn retrieval exposed to Gemini Live as a `search_knowledge` function tool, and whole-document injection for corpora that fit the context window.
- **In-game HUD.** A dual-panel overlay for `GeminiLiveAgent` with a live transcription panel and a settings panel — reconnect, microphone and camera selection with preview, vision on/off, stream frequency, interruption and echo handling. Both panels drag and resize at runtime.
- **Setup wizard.** A unified `IVA SDK` menu handling dependencies, credentials, and sanity checks in one window.
- **Structured logging and typed exceptions.** `IVALogger` with severity and category filtering, plus an `IVAException` hierarchy so failures are catchable by type instead of by string-matching the Console.
- **Long-term memory, session recording, and metrics**, and `AgentPreset` assets for serializing a whole agent configuration.
- **Realtime session fixes.** Vertex AI previously could not connect at all when a tool was attached; thinking was only disabled on Vertex, making AI Studio noticeably slower; and reconnects did not refresh the Vertex token. All three are fixed with no public API change.

### Main features
The image above shows the interaction loop of a conversational virtual agent. 

* Conversational intelligent virtual agents with human-AI interaction loop.
    - <b>multimodal prompting</b> : combining text-based user message with system prompt that contains a custom list of possible agent actions and facial expressions, with optional image prompt as support. 
    - <b>structure output from LLM/VLM</b>, containing selected action, facial expression, and text response.
    - <b>realistic IVA behavior</b> combining the multimodal output, including gaze, action, and facial expressions. 


If you want to have more modularized cloud services (e.g. using different STT, LLM, TTS models), checkout [documentation for v1.0](https://github.com/uhhhci/intelligent-virtual-agent-sdk/blob/v3.0.0/READMEv1.0.0.md)


## Quick Start

#### Simple Agent- Quick Start
- An example agent can be added by creating an empty Gameobject in the scene and adding the script: `Packages/de.uhh.hci.ivh.core/Runtime/Scripts/IntelligentVirtualAgent/GeminiLiveAgent.cs` to it. 
    
- In its field "Agent Prefab" you can drag e.g.: `Packages/de.uhh.hci.ivh.core/Runtime/Models/Rocketbox/Business_Female_01/Export/Business_Female_01_facial.fbx`.

- In its field "Animator Controller" you need to drag in `Packages/de.uhh.hci.ivh.core/Runtime/AnimationControllers/RocketboxFemale.controller`.

- In the Emotion Handler Type, choose FACS. If you want more diverse and sophisicated facial expression animations, and if you have a license for CC4 digital soul facial expression animation database, see [CC4 Characters and Animations](#reallusion-animation).

- In the CharacterType, choose ``Rocketbox`` if you are using rocketbox character. Choose ``CC4orDIDIMO`` otherwise. 


- Any ``Additional Description`` will be added to the IVA's system prompt. 


- After you added both the Animator Controller and the agent model, click on `Setup Agent` in the Editor.


- Add the `Packages/de.uhh.hci.ivh.core/Runtime/Prefabs/PreviewScenePrefab.prefab` to the scene for better lighting and appearance of the scene. 

- Two ready-made scenes ship with this project:
    - `Assets/Scenes/GeminiLiveStreamAgentWebcamRocketbox.unity` — a realtime voice **and** vision agent that sees through your webcam.
    - `Assets/Scenes/GeminiVoiceOnlyAgent.unity` — a voice-only agent, useful when you have no camera or want lower bandwidth.

- Further sample scenes are shipped inside the package itself. Import them from **Window → Package Manager → Intelligent Virtual Human SDK Core → Samples**; they land under `Assets/Samples/Intelligent Virtual Human SDK Core/3.0.0/`.

## Grounding an agent in documents

New in v3.0.0: an agent can answer from your own documents rather than from the model's pre-training. Two of the shipped samples demonstrate the extremes, and both use entirely fictional corpora:

| Sample | Strategy | Setup required |
| :--- | :--- | :--- |
| **Knowledge Grounding — Long Document via Prompt** | Injects the whole document into the prompt at session start. No chunking, embedding, or baking; every fact is always in context. | API key only |
| **Knowledge Grounding — RAG Retrieval (Gemini Embeddings)** | Per-turn retrieval over a baked corpus. The agent calls a `search_knowledge` tool and only the top-K chunks enter the prompt, so the corpus can exceed the context window. | Requires baking the `KnowledgeBase` asset first |

Start with the long-document sample — it needs nothing beyond a Gemini key. Move to RAG when your corpus outgrows the context window. The full guide, including chunking parameters, citation formatting, and how to swap in your own embedder or vector store, is in [Grounding an agent in documents](https://github.com/uhhhci/intelligent-virtual-agent-sdk/blob/v3.0.0/Documentations~/howToGroundAgentInDocuments.md).


## Connect to Gemini Live Cloud Service

The current implementation supports 3 different live model. Two free-tier models from google AI studio and one paid model from Google's Vertex AI. 

| Model Variant | Source | Tier | Latency | Model ID / Notes |
| :--- | :--- | :--- | :--- | :--- |
| **Gemini Live 2.5 Flash** | Vertex AI | Paid | Low | [`gemini-live-2.5-flash-native-audio`](https://docs.cloud.google.com/vertex-ai/generative-ai/docs/live-api) |
| **Gemini Live 2.5 Flash** | Google AI | Free | High | [`gemini-2.5-flash-native-audio-preview-12-2025`](https://ai.google.dev/gemini-api/docs/live?example=mic-stream) |
| **Gemini 2.0 Flash Exp** | Google AI | Free | Low | `gemini-2.0-flash-exp`<br>*(To be deprecated/terminated in March 2026)* |

### Option 1: Google AI Studio (Quick Start)
*Recommended for individual developers and prototyping. Please check [Google AI Studio Documentations](https://ai.google.dev/gemini-api/docs/pricing#gemini-2.5-flash-native-audio) for further details.*

1.  **Get the Key:**
    * Visit [Google AI Studio](https://aistudio.google.com/).
    * Click **Get API key** in the left sidebar.
    * Click **Create API key**.

2.  **Create the Auth File:**
    * Navigate to your `.aiapi` directory.
    * Create a file named `auth.json`.
    * Paste the following JSON content:

    ```json
    {
        "gemini_api_key": "PASTE_YOUR_API_KEY_HERE"
    }
    ```

### Option 2: Google Cloud Vertex AI (Enterprise)
*Recommended for production applications requiring higher rate limits and strict data compliance.*

#### 1. Enable the API
1.  Go to the [Google Cloud Console](https://console.cloud.google.com/).
2.  Select your project (or create a new one).
3.  In the top search bar, type **"Vertex AI API"**.
4.  Select it from the Marketplace results and click **Enable**.

#### 2. Create a Service Account
1.  In the search bar, type **"Service Accounts"** (found under **IAM & Admin**) and open it.
2.  Click **+ CREATE SERVICE ACCOUNT** at the top.
3.  **Step 1 (Details):** Enter a name (e.g., `unity-gemini-app`) and click **Create and Continue**.
4.  **Step 2 (Permissions):**
    * Click the **Select a role** filter.
    * Type **"Vertex AI User"** and select it.
    * *Note: This specific role is required to invoke models.*
    * Click **Continue** and then **Done**.

#### 3. Generate and Save Credentials
1.  In the Service Accounts list, click the **Email address** of the account you just created.
2.  Click the **Keys** tab in the top navigation bar.
3.  Click **Add Key** → **Create new key**.
4.  Select **JSON** and click **Create**.
5.  A JSON file will download to your computer.

#### 4. Install the Credential File
1.  Rename the downloaded file to: `service-account.json`.
2.  Move the file into your configuration directory:
    `C:\Users\YOUR_USERNAME\.aiapi\`

### ✅  Verification
Your `.aiapi` folder should contain one of the following files depending on your chosen method:

* `auth.json` (for AI Studio)
* `service_account.json` (for Vertex AI)

## Documentation

- For the full Documentation, visit the [Wiki](https://github.com/uhhhci/intelligent-virtual-agent-sdk/wiki).
- [Release notes for v3.0.0](https://github.com/uhhhci/intelligent-virtual-agent-sdk/blob/v3.0.0/CHANGELOG.md)
- [How to ground an agent in your own documents](https://github.com/uhhhci/intelligent-virtual-agent-sdk/blob/v3.0.0/Documentations~/howToGroundAgentInDocuments.md)
- [How to add more/custom animations to IVA actions](https://github.com/uhhhci/intelligent-virtual-agent-sdk/blob/v3.0.0/Documentations~/howToAddMoreAnimations.md)
- [How to develop the package while using it in Unity](https://github.com/uhhhci/intelligent-virtual-agent-sdk/blob/v3.0.0/Documentations~/howToDevelopPackage.md)

## [DIDIMO](https://www.didimo.co/) Character License Notice

 The DIDIMO asset is licensed solely for use within this repository and only to the extent necessary to build, test, and demonstrate this toolkit. You must not: resell the asset, redistribute the asset separately from this repository, or recreate, extract, or adapt the asset for use in any other project, product, or context.  See the detail [License](https://github.com/uhhhci/intelligent-virtual-agent-sdk/blob/v3.0.0/LICENSE.md). 

## Rocketbox Characters License Notice

This repository includes 3D character assets sourced from the Rocketbox Avatar Library, originally developed by Rocketbox Studios and later made freely available by Microsoft for academic and non-commercial use.

The Rocketbox character models are provided under a **non-commercial license** and are intended **solely for academic, research, and educational purposes**. Use of these assets is subject to the original Rocketbox EULA provided by Microsoft, which can be found here:

https://github.com/microsoft/Microsoft-Rocketbox#license


## Mixamo Animations

Our package provides full support for mixamo animations. However, due to Adobe's redistribution limitation, we can not include the animation setup in the report. If you would like to use the different animations from Mixamo for your agents for your non-commerical research and academic work, please contact us. <b>We will be happy to provide you with a Unity-compatible animation package asset that has already been imported and configured, saving you the setup effort. </b>

## Reallusion Animation

The facial expressions of the Intelligent Virtual Agents (IVAs) you migh thave seen in many demo videos in this project are animated using Reallusion’s Digital Soul asset library, which is protected under a restricted usage license. 

As per Reallusion’s licensing terms, we are not permitted to redistribute this asset directly through the repository. However, if you hold a valid license and seats for Reallusion’s Digital Soul, please contact us. <b>We will be happy to provide you with a Unity-compatible animation asset that has already been imported and configured, saving you the setup effort. </b> 📧 For licensed access, contact the maintainence team. 

### Maintainer
Name: Ke Li, Sebastian Rings , Julia Hertel, Michael Arz<br>
Mail: ke.li@uni-hamburg.de, sebastian.rings@uni-hamburg.de, julia.hertel@uni-hamburg.de, michael.arz@uni-hamburg.de

### License
This toolkit is released for academic and research purposes only, free of charge. For commercial use, a seperate license must be obtained.  Please find detailed licensing information [here](https://github.com/uhhhci/intelligent-virtual-agent-sdk/blob/v3.0.0/LICENSE.md)

### Citation
If this work helps your research, please cite the following papers:

```
@article{LiAnthromorphicAI2026,
  author  = {Li, Ke and Mostajeran, Fariba and Rings, Sebastian and Hertel, Julia and Schmidt, Susanne and Arz, Michael and Steinicke, Frank},
  title   = {Anthropomorphic AI: A Toolkit for Authoring and Interacting with Intelligent Virtual Agents for Extended Reality},
  journal = {Frontiers in Virtual Reality},
  year    = {2026},
  volume  = {7},
  doi     = {10.3389/frvir.2026.1794720}
}

@article{Li2025IHS,
  title={I Hear, See, Speak \& Do: Bringing Multimodal Information Processing to Intelligent Virtual Agents for Natural Human-AI Communication},
  author={Ke Li and Fariba Mostajeran and Sebastian Rings and Lucie Kruse and Susanne Schmidt and Michael Arz and Erik Wolf and Frank Steinicke},
  journal={2025 IEEE Conference on Virtual Reality and 3D User Interfaces Abstracts and Workshops (VRW)},
  year={2025},
  pages={1648-1649},
  url={https://api.semanticscholar.org/CorpusID:278063630}
}

@article{Mostajeran2025ATF,
  title={A Toolkit for Creating Intelligent Virtual Humans in Extended Reality},
  author={Fariba Mostajeran and Ke Li and Sebastian Rings and Lucie Kruse and Erik Wolf and Susanne Schmidt and Michael Arz and Joan Llobera and Pierre Nagorny and Caecilia Charbonnier and Hannes Fassold and Xenxo Alvarez and Andr{\'e} Tavares and Nuno Santos and Jo{\~a}o Orvalho and Sergi Fern{\'a}ndez and Frank Steinicke},
  journal={2025 IEEE Conference on Virtual Reality and 3D User Interfaces Abstracts and Workshops (VRW)},
  year={2025},
  pages={736-741},
  url={https://api.semanticscholar.org/CorpusID:278065150}
}
```


## Acknowledgement 

This work has received funding from the European Union’s Horizon Europe research and innovation program under grant agreement No 101135025, PRESENCE project. 
