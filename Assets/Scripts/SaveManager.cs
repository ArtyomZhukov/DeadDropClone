using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string fileName = "save.dds";
    private static string path;

    public static RecordsData recordsData = new RecordsData();

    private static RSACryptoServiceProvider rsaCryptoServiceProvider;
    private const string privateKey = "<RSAKeyValue><Modulus>252QgPZoiStRBuN95SqnArEoh4MQEiTgbgDPWQo4knNCLhecH4swFvS+dQmXwPrS6Nw+7SLiz6vbFkixMbcYM74bMn2mQPFq/cf3OQrELP69MzTtQkAZHX8vOqNR263c5gOv7Gzcj34QiL29X/xVPXehCx6IDJX9F56JLFZKoP/Hqrt82hI7qA3OZtqieBFcFYu4N+dh8bUgIRE3mhLl+IyPQOtnZNGC/qEFOI1AIOj0cH6PpNCT4qBNcp07aO1CEEhi0oMBgY5N5MHCPnzTBgtDF60z0qrj5rMlpoCoaclm2IYMnl5DghpnOsXeMWfOWClmhAdiyvxxIfyW5meU7xcnQEt1D5ItbzXOTJtKgjZBV0cJxdqCVBJf+2sOxWUK+ELaRiesifUbRHJsP1LWYiA1/PhgI5npxtffR4A48DK2+vR2YzzIY6x/H216OTCzSpSI5SvNrnM2ayYw0Z7y+TxifCTGMAT/Zxhi64usmfm+gcJZI55OAnpiT4Vcpp/mleZXCAdtTzOcwmJNNx62456BW0znM3w8D1vst3+CJKB9KU16NHJVOa0rv5q0y9rfWaWmw6xKNjIMLaItBRksPt/PSwwt/c+/riJzkYo8/KPp23cetLwwnoxWXzrOqUAQazLzap/AOoLPRr7d1JnJmOB+yHgQRYYXoUOvNwkOyXU=</Modulus><Exponent>AQAB</Exponent><P>9SxrYmoyRwGmFMfeF+D4kcdTgFX8I4+p5C6B86wc8n/Hdu72tSBf5kE3HgdaE+4SKkoEtLC9zqgZ+W4fUnpjdwmIim4KDN82ldzxRmloPobGZ0xWRQwRwcpgM2daj+t+4XFa810WSEoP9UNUJ1LLvZvbM3X1oO0HUAzuvHOZwcLQkcKRu2o3Am1fH2dSPsWmP6pXWZ3z2SeAuMkYC2u5RcTUItzv0c/uXNHp15d6hjRGZpt2z1KU6plGXXTCyEZtLa2PCg90soaAsR6Vj1G4H1KAoy4CHMJnVp9/R6m3W5tqvB/iAsqNXz26bifZKVT/eT+qK5h6jBvjZ/2i8AWaqw==</P><Q>5VA4+TEJSf/np5gqT5Xhmck3TgbeCq7WnqM02qWNv7D9gBPk7D8QMRhcssZpdrbKM4rQ0LqPYTkEjs64OnYq0J2ygsm+Sfb+FuHJiHCHhpvHZJamXumPX1BkEY6Nr+iqiK5G38lb4sPTcwit4b2obSbSA7X/hWIDOm9RNUqzWq19FdkWhca9dZotkyJTZq8ZiUk6Wzv+CVMsx4oGqWn/pQNfHA3NTJ7P74WGDaf2ACkb3AReH88gTyH6e8Qvd57AZYZQxxvqeCcBqYTsqlSRSNg1vU1Z6nYA9qwOfvxBVpWp2C1mEccOy5v/ddZhpzX814IJFJulQDAnZ96W0S8sXw==</Q><DP>Yg8WFBKtcUDzkDQwXBSa1pOGjjnV0tNO+/it+SvJmCQLP0JsYh1Eve8vgE8oc/gwQ8G/CBX4lIMfgGfPF0nkJmQlvFgupN9SEbaAuczG6Ns213Hsv0kIgR3URDr+ObnS0ZBo9BUzKE6W+12mduPioT+I+JghWe/zkMzbX3xM7vZgPSw45WCiB9J+zSyh9IFQ8P3MWgeW1C+8iCvF9itrur/yqM79DEnB6FX81UG8u5iE390mM92vufT7870Tes7QNAjKwRdVcWHibvTwlJX2snBpCtsH9UC73CWG3r6+m2EZ24xU9RDWYlVqD0zYynM4iuIvyaf+u7MbzSfg12cmaQ==</DP><DQ>iejDOuqIwZ5LvXlACPkE4q4muqBarWYU4PcadFyYcS6KZpqQVJxgqoYSSdoV1zV7SaA0kTOBw2C9Iv2jwlUKzsfoccvOy0Dl8vD7eTjj6MqLEi2gBPjGJvZ8GCr5f0+YL/dP3IqG3kwTQN15ZYfgYTS1nVG5SIh3lI8Z8cjpMKGh1p/mcbHig3Wj7xb3vYuU+gG2PORJlWYWSjLyarS68IykiISscWYZe917j37x7YrLnHhr+wlgTvcKY6DLQ0+QOf6bWgOp8XTUBRVSF+YgMgUF3MFGUEGDttYnKaufx+jeaopzFkH23fEFo+K4fA118E3eFCy/J2lOaaNuZIPQVQ==</DQ><InverseQ>axKRDvjMBuP3qb+A75RO8YJ+msxIAwQ7xlyw4WLRUC1YSigr7Wqq+J2BpBUwbPflkP+LJQW6EWnBPFrIiVMFRR/q1ADmXQ1zneUdUNo0j4epXvseVYSRjoIfFc/xZgbIq4wBH/CqGivPLDjwJ/SoJ83QA5jxCoSIfcTBsDkhwXNFv54DROFtVyVYI9CdyH+QHUt3Xu0CUhnNAWE7PUlaTuMn/9SiVq+YzpsiKF4lzpvDrl6kjkKx5REHgQKJU25tSvZIPM6tnW9MbkN7RX2kFIRDlb2EWGB8Hu96n7Sn993JshRKe4b6bzb9me2FAkd+u2FQ1AeHaurCT2epJQPeoA==</InverseQ><D>Em46vlfvlfNorWdSHXVi5rrvZ8/7/UnS++hdfl95N/EsUKnylEMeGg8YHMJdVfi20owJS+Vtm3gUvt4C4OU4xr7hkW8yEKkyDXJGsSbSJ+SJBE8gyjqO4QbXc9hY+lKxAyy0Cudn6zLtOEyUJAYNMUdk4IYDbOr5fo3zfSEQykos+4sYg0ScdRITjFY05Fawn65GG4rzkVFeDOhg/jEv05gYPZ6D4tYiZDQnOAPCqgBi4z372B0ZDB/f4AqJ/dhLfwpUIM6VxcWni9cm8g+PtiYjz0zd2IQ29Bl8w2hwudAJLYqybGgEylpk523Lo+HOU1/GFrZ3+AkPhYapM1TmDrLUrpmGcmBWymlic57V2HhtNfu6p1yhpJOboO52gFMaNMvcnDgBKHaP1r75ifHXFh6mAKWJHqVvbJCiVTrZjD4p6DUD8AP0OzWDc+5MDV1VnEJKiiU8YIHPiEXGp+cvssk2ixPmkAxGVILmqVoouJOSVtgHRqsTXsNnT09YLrL1rkJ1fygn0IWFlCfYBdFjV5aIe66jDipVLF09mrEHn1J0iWHXcHA+G4SYzMexvaRt89D/t/FwAMLWJ8kFkr9hU3ZWZL1jDuVTjRELyP0+aHRPKlGoC73ERukrg0B4WbjOUXqm/qcnlmpTVSrZMRLOil0VxT4W36OkDlBSGKd7CFE=</D></RSAKeyValue>";
    private const string publicKey = "<RSAKeyValue><Modulus>252QgPZoiStRBuN95SqnArEoh4MQEiTgbgDPWQo4knNCLhecH4swFvS+dQmXwPrS6Nw+7SLiz6vbFkixMbcYM74bMn2mQPFq/cf3OQrELP69MzTtQkAZHX8vOqNR263c5gOv7Gzcj34QiL29X/xVPXehCx6IDJX9F56JLFZKoP/Hqrt82hI7qA3OZtqieBFcFYu4N+dh8bUgIRE3mhLl+IyPQOtnZNGC/qEFOI1AIOj0cH6PpNCT4qBNcp07aO1CEEhi0oMBgY5N5MHCPnzTBgtDF60z0qrj5rMlpoCoaclm2IYMnl5DghpnOsXeMWfOWClmhAdiyvxxIfyW5meU7xcnQEt1D5ItbzXOTJtKgjZBV0cJxdqCVBJf+2sOxWUK+ELaRiesifUbRHJsP1LWYiA1/PhgI5npxtffR4A48DK2+vR2YzzIY6x/H216OTCzSpSI5SvNrnM2ayYw0Z7y+TxifCTGMAT/Zxhi64usmfm+gcJZI55OAnpiT4Vcpp/mleZXCAdtTzOcwmJNNx62456BW0znM3w8D1vst3+CJKB9KU16NHJVOa0rv5q0y9rfWaWmw6xKNjIMLaItBRksPt/PSwwt/c+/riJzkYo8/KPp23cetLwwnoxWXzrOqUAQazLzap/AOoLPRr7d1JnJmOB+yHgQRYYXoUOvNwkOyXU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

    void Awake()
    {
        path = Application.persistentDataPath + "/" + fileName;
        Load();
    }

    public static void NewRecord(bool spy, string name, int time)
    {
        var records = (spy ? recordsData.spyRecords : recordsData.sniperRecords);
        if (records.Count >= 20)
        {
            records.RemoveAt(records.Count - 1);
        }
        records.Add(new RecordsData.Record(name, time));
        Save();
    }

    private static void SortRecords()
    {
        recordsData.sniperRecords.Sort((x, y) => x.time.CompareTo(y.time));
        recordsData.spyRecords.Sort((x, y) => x.time.CompareTo(y.time));
    }

    public static bool CheckRecord(bool spy, int time)
    {
        foreach (var item in (spy ? recordsData.spyRecords : recordsData.sniperRecords))
        {
            if (item.time > time)
            {
                return true;
            }
        }
        return (spy ? recordsData.spyRecords : recordsData.sniperRecords).Count < 19;
    }

    public static void Save()
    {
        SortRecords();
        string contents = JsonUtility.ToJson(recordsData);
        System.IO.File.WriteAllText(path, Encrypt(contents));
    }

    public static void Load()
    {
        try
        {
            if (System.IO.File.Exists(path))
            {
                string contents = System.IO.File.ReadAllText(path);
                recordsData = JsonUtility.FromJson<RecordsData>(Decrypt(contents));
            }
        }
        catch (Exception)
        {
        }
    }

    private static string Encrypt(string data)
    {
        rsaCryptoServiceProvider = new RSACryptoServiceProvider(4096);

        int textBlock = 250;
        rsaCryptoServiceProvider.FromXmlString(publicKey);
        int textLenth = data.Length;
        string result = "";
        byte[] plainText, encrypted;

        if (textLenth < textBlock)
        {
            plainText = Encoding.Unicode.GetBytes(data);
            encrypted = rsaCryptoServiceProvider.Encrypt(plainText, false);
            result = Convert.ToBase64String(encrypted);
        }
        else
        {
            for (int i = 0; i < data.Length; i += textBlock)
            {
                plainText = Encoding.Unicode.GetBytes(data.Substring(i, i + textBlock > textLenth ? textLenth - i : textBlock));
                encrypted = rsaCryptoServiceProvider.Encrypt(plainText, false);
                result += Convert.ToBase64String(encrypted);
            }
        }
        return result;
    }

    private static string Decrypt(string data)
    {
        try
        {
            rsaCryptoServiceProvider = new RSACryptoServiceProvider(4096);
            rsaCryptoServiceProvider.FromXmlString(privateKey);

            var splits = Regex.Split(data, @"([^=]+[=]+)").Where(p => !string.IsNullOrEmpty(p)).ToList();
            string result = "";
            foreach (var cryptoblock in splits)
            {
                byte[] x = (Convert.FromBase64String(cryptoblock));
                byte[] decrypted = rsaCryptoServiceProvider.Decrypt(x, false);
                result += Encoding.Unicode.GetString(Convert.FromBase64String(Convert.ToBase64String(decrypted)));
            }
            return result;
        }
        catch (Exception)
        {
            return "";
        }
    }
}

[Serializable]
public class RecordsData
{
    public List<Record> spyRecords = new List<Record>();
    public List<Record> sniperRecords = new List<Record>();

    [Serializable]
    public class Record
    {
        public string name;
        public int time;

        public Record(string Name, int Time)
        {
            name = Name;
            time = Time;
        }
    }
}
