﻿using ConsoleApp2;
using ConsoleApp2.Midi;
using ConsoleApp2.Plugins.Fx.Basic;
using ConsoleApp2.Plugins.Fx.Distortion;
using ConsoleApp2.Plugins.Instrument;
using ConsoleApp2.Utils;
using NAudio.Wave;

Pool.Audio.Add(FileManage.LoadWaveFromFile("Samples\\Drums\\FL_PSY24_Top_Loop_140_01.wav"));
Mixer mixer = new Mixer();
mixer.BufferSize = 44100;
Channel kick = new Channel();
MidiData data1 = new MidiData();
data1.Note = 36;
data1.Velocity = 1;
data1.SamplePosition = Measure.SamplePosition(140, 44100, 0, 4, 0);
data1.SampleLength = (uint)Pool.Audio[0].GetLength(1) - 12000;
kick.MidiData.Add(data1);
Sampler kickSampler = new Sampler();
kickSampler.AudioIndex = 0;
kickSampler.EndPosition = (uint)Pool.Audio[0].GetLength(1) - 1;
kick.Plugins.Add(kickSampler);
mixer.Channels.Add(kick);
BitReduction g = new BitReduction();
g.Depth = 15;
kick.Plugins.Add(g);
FileManage.SaveWaveFromMixer("wav", mixer, 3);
FileManage.PlayWave("wav");
File.Delete("wav.wav");
