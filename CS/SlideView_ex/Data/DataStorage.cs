using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.Generic;
using System;
using System.IO;

namespace Data {
    [XmlRoot("Writers")]
    public class Writers : List<Writer> {
    }
    [XmlRoot("Writer")]
    public class Writer {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }
        public string Occupation { get; set; }
        public string Genres { get; set; }
        public string Notes { get; set; }
        public byte[] ImageData { get; set; }
        BitmapImage imageSource;
        [XmlIgnore]
        public BitmapImage ImageSource {
            get {
                if(imageSource == null) {
                    SetImageSource();
                }
                return imageSource;
            }
        }
        async void SetImageSource() {
            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
            await stream.WriteAsync(ImageData.AsBuffer());
            stream.Seek(0);

            imageSource = new BitmapImage();
            imageSource.SetSource(stream);
        }
    }

    public static class DataStorage {
        static Writers writers;
        public static Writers Writers {
            get {
                if (writers == null) {
                    StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/Writers.xml")).AsTask().Result;

                    Stream stream = file.OpenStreamForReadAsync().Result;
                    XmlSerializer serializier = new XmlSerializer(typeof(Writers));
                    writers = serializier.Deserialize(stream) as Writers;
                }
                return writers;
            }
        }
    }
    public class WritersData {
        public Writers DataSource {
            get { return DataStorage.Writers; }
        }
    }
}