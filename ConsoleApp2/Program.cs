using ConsoleApp2;
using ConsoleApp2.Midi;
using ConsoleApp2.Plugins.Instrument;
using ConsoleApp2.Utils;
using NAudio.Wave;

Pool.Audio.Add(FileManage.LoadWaveFromFile("Samples\\Drums\\Kicks\\Kick1 A#.wav"));
Mixer mixer = new Mixer();
mixer.BufferSize = 44100;
Channel kick = new Channel();
MidiData data1 = new MidiData();
data1.Note = 36;
data1.Velocity = 1;
data1.SamplePosition = 0;
data1.SampleLength = (uint)Pool.Audio[0].GetLength(1) -1;
kick.MidiData.Add(data1);
Sampler kickSampler = new Sampler();
kickSampler.AudioIndex = 0;
kickSampler.EndPosition = data1.SampleLength;
kick.Plugins.Add(kickSampler);
mixer.Channels.Add(kick);

WaveFileWriter w;

float[] buf = mixer.ProcessBuffer();
w = new WaveFileWriter("wav2.wav", new WaveFormat(44100, 16, 2));
w.WriteSamples(buf, 0, buf.Length);
w.Close();

while(true)
{
    Console.WriteLine("Finish");
    Thread.Sleep(10);
}
