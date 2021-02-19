using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using KaupischIT.CardReader.Egk.PersoenlicheVersichertendaten;

namespace KaupischIT.CardReader
{
	/// <summary>
	/// Stellt Daten einer elektronischen Gesundheitskarte (eGK) bereit
	/// </summary>
	public class EgkResult
	{
		/// <summary>
		/// Ruft die Persönlichen Versichertendaten (PD) aus den Versichertenstammdaten einer eGK ab
		/// </summary>
		public PersoenlicheVersichertendaten PersoenlicheVersichertendaten { get; private set; }

		private CardTerminalClient.LogSink _debugSink;

		/// <summary>
		/// Initialisiert eine neue Instanz der EgkResult-Klasse und dekodiert die übergebenen Versichertenstammdaten einer eGK
		/// </summary>
		/// <param name="pdData">die Rohdaten mit den Personendaten (PD)</param>
		/// <param name="debugSink">Ausgabesenke fuer Debug-Informationen</param>
		public EgkResult(byte[] pdData, CardTerminalClient.LogSink debugSink = null)
		{
			this._debugSink = debugSink;
			this.DecodePD(pdData);
		}


		private void DecodePD(byte[] bytes)
		{
			int length = (bytes[0]<<8) + bytes[1]; // die ersten beiden Bytes der Rohdaten beinhalten die Längenangabe für die eigentlichen Nutzdaten

			// Nutzdaten extrahieren...
			byte[] compressedData = new byte[length];
			Array.Copy(bytes,2,compressedData,0,compressedData.Length);

			// ...und dann deserialisieren
			this.PersoenlicheVersichertendaten = this.Decompress<PersoenlicheVersichertendaten>(compressedData);
		}

		/// <summary>
		/// Dekomprimiert und deserialisiert die ZIP-komprimierten XML-Nutzdaten (ISO-8859-15 codiert)
		/// </summary>
		/// <typeparam name="T">Der Objekttyp, den der XmlSerializer serialisieren soll</typeparam>
		private T Decompress<T>(byte[] compressedData)
		{
			// die Daten selbst werden als...
			using (MemoryStream memoryStream = new MemoryStream(compressedData))
			using (GZipStream gzipStream = new GZipStream(memoryStream,CompressionMode.Decompress)) // ...ZIP-Komprimiertes,...
			using (StreamReader streamReader = new StreamReader(gzipStream,Encoding.GetEncoding("ISO-8859-15"))) /// ...ISO-8859-15 codiertes..
			{
				// ...XML-Dokument abgelegt
				string xmlContent = streamReader.ReadToEnd();
#if DEBUG || DEBUG_LOG
				this._debugSink?.Invoke(xmlContent);
#endif

				// XML-Daten gemäß vorgegebenem XML-Schema deserialisieren
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				using (TextReader textReader = new StringReader(xmlContent))
				using (XmlTextReader xmlTextReader = new SkipNamespaceReader(textReader)) // die verschiedenen Schema-Versionen (5.1, 5.2) haben unterschiedliche XML-Namespaces - diese hier ignorieren
					return (T)xmlSerializer.Deserialize(xmlTextReader);
			}
		}

		private sealed class SkipNamespaceReader : XmlTextReader
		{
			public SkipNamespaceReader(TextReader reader) : base(reader)
			{
				// Namespace-Support muss aktiviert bleiben, sonst kracht es bei z.B. mit IKK-karten
				this.Namespaces = true;
			}

			// Namespace wird auf den Leerstring == kein NS hardcodiert
			public override string NamespaceURI => String.Empty;
		}
	}
}
