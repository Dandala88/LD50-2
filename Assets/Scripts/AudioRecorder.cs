using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class AudioRecorder : MonoBehaviour
{
    FileStream filestream;
    private int bufferSize;
    private int numBuffers;
    private int outputRate = 44100;
    private int headerSize = 44; //default for uncompressed wav
    public void Awake()
    {
        StartWriting();
    }

    private void StartWriting()
    {
        if (filestream == null)
        {
            filestream = new FileStream("ripple.wav", FileMode.Create);
            byte emptyByte = new byte();

            for (int i = 0; i < headerSize; i++)
            {
                filestream.WriteByte(emptyByte);
            }
        }
    }

    public void ConvertAndWrite(float[] data)
    {
        if (filestream != null)
        {
            Int16[] intData = new Int16[data.Length];

            Byte[] bytesData = new Byte[data.Length * 2];

            int rescaleFactor = 32767;

            for (int i = 0; i < data.Length; i++)
            {
                intData[i] = (Int16)(data[i] * rescaleFactor);
                Byte[] byteArr = new Byte[2];
                byteArr = BitConverter.GetBytes(intData[i]);
                byteArr.CopyTo(bytesData, i * 2);
            }

            filestream.Write(bytesData, 0, bytesData.Length);
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        ConvertAndWrite(data);
    }

    private void WriteHeader()
    {

        filestream.Seek(0, SeekOrigin.Begin);

        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        filestream.Write(riff, 0, 4);

        Byte[] chunkSize = BitConverter.GetBytes(filestream.Length - 8);
        filestream.Write(chunkSize, 0, 4);

        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        filestream.Write(wave, 0, 4);

        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        filestream.Write(fmt, 0, 4);

        Byte[] subChunk1 = BitConverter.GetBytes(16);
        filestream.Write(subChunk1, 0, 4);

        UInt16 two = 2;
        UInt16 one = 1;

        Byte[] audioFormat  = BitConverter.GetBytes(one);
        filestream.Write(audioFormat, 0, 2);

        Byte[] numChannels = BitConverter.GetBytes(two);
        filestream.Write(numChannels, 0, 2);

        Byte[] sampleRate = BitConverter.GetBytes(outputRate);
        filestream.Write(sampleRate, 0, 4);

        Byte[] byteRate = BitConverter.GetBytes(outputRate * 4);
        // sampleRate * bytesPerSample*number of channels, here 44100*2*2

        filestream.Write(byteRate, 0, 4);

        UInt16 four = 4;
        Byte[] blockAlign  = BitConverter.GetBytes(four);
        filestream.Write(blockAlign, 0, 2);

        UInt16 sixteen = 16;
        Byte[] bitsPerSample = BitConverter.GetBytes(sixteen);
        filestream.Write(bitsPerSample, 0, 2);

        Byte[] dataString = System.Text.Encoding.UTF8.GetBytes("data");
        filestream.Write(dataString, 0, 4);

        Byte[] subChunk2 = BitConverter.GetBytes(filestream.Length - headerSize);
        filestream.Write(subChunk2, 0, 4);

        filestream.Close();
        filestream = null;
    }

    private void OnEnable()
    {
        GameManager.OnStartGame += StartWriting;
        GameManager.OnEndGame += WriteHeader;
    }

    private void OnDisable()
    {
        GameManager.OnStartGame -= StartWriting;
        GameManager.OnEndGame -= WriteHeader;
    }

}
