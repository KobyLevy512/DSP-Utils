using ConsoleApp2.Utils;
using NAudio.Wave;
WaveFormat fmt = new WaveFormat(44100, 32, 2);
BufferedWaveProvider buffer = new BufferedWaveProvider(fmt);
WaveOutEvent e = new WaveOutEvent();