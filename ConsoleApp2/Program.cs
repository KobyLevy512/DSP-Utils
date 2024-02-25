using ConsoleApp2;
using ConsoleApp2.Midi;
using ConsoleApp2.Plugins.Instrument;
using NAudio.Wave;

BinaryWriter wr = new BinaryWriter(File.Create("asd"));
wr.Write((ushort)255);
wr.Close();
//reader.
//Pool.Audio.Add();
//Mixer mixer = new Mixer();
//Channel kick = new Channel();
//MidiData data1 = new MidiData();
//data1.Note = 36;
//data1.Velocity = 1;
//data1.SamplePosition = 
//kick.MidiData.Add(new MidiData());
//Sampler kickSampler = new Sampler();
//kickSampler.AudioIndex = 0;
//kick.Plugins.Add(kickSampler);
//mixer.Channels.Add(kick);
//WaveFormat fmt = new WaveFormat(44100, 32, 2);
//BufferedWaveProvider buffer = new BufferedWaveProvider(fmt);
//WaveOutEvent e = new WaveOutEvent();