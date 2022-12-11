using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using GMap.NET.WindowsForms;
using GMap.NET;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;

namespace course
{
    public partial class Map : Form
    {
        // хардкод - это плохо!!!
        private const double FromX = 55.994474805136846;
        private const double FromY = 92.79788544574697;

        private readonly double TargetX;
        private readonly double TargetY;
        public Map(double target_x, double target_y)
        {
            InitializeComponent();

            TargetX = target_x;
            TargetY = target_y;

            Setting();
            DrawMap();
        }

        private void DrawMap()
        {
            GMapOverlay routes = new GMapOverlay("routes"); //Создаем объект наложения (Overlay)
            List<PointLatLng> points = new List<PointLatLng>(); //Создаем лист, где будут наши точки пути.

            PointLatLng start = new PointLatLng(FromX, FromY);
            PointLatLng end = new PointLatLng(TargetX, TargetY);
            points.Add(start);
            points.Add(end);

            //MapRoute route = GoogleMapProvider.Instance.GetRoute(start, end, false, true, 14);

            GMapRoute r = new GMapRoute(points, "Путь доставки");
            r.Stroke = new Pen(Color.Red, 3); //Задаем цвет и ширину линии
            routes.Routes.Add(r); //Добавляем на наш Overlay маршрут
            gmap1.Overlays.Add(routes); //Накладываем Overlay на карту.

            GMapOverlay markersOverlay = new GMapOverlay("marker"); //Создаем Overlay
            GMarkerGoogle markerStart = new GMarkerGoogle(points.FirstOrDefault(), GMarkerGoogleType.blue); //Создаем новую точку и даем ей координаты первого элемента из листа координат и синий цвет
            GMarkerGoogle markerEnd = new GMarkerGoogle(points.LastOrDefault(), GMarkerGoogleType.red); //Тоже самое, но красный цвет и последний из списка координат.
            markerStart.ToolTip = new GMapRoundedToolTip(markerStart); //Указываем тип всплывающей подсказки для точки старта
            markerEnd.ToolTip = new GMapBaloonToolTip(markerEnd); //Другой тип подсказки для точки окончания (для теста)
            markerStart.ToolTipText = "Точка старта"; //Текст всплывающих подсказок при наведении
            markerEnd.ToolTipText = "Точка окончания";

            markersOverlay.Markers.Add(markerStart); //Добавляем точки
            markersOverlay.Markers.Add(markerEnd); //В наш оверлей маркеров

            gmap1.Overlays.Add(markersOverlay); //Добавляем оверлей на карту

            // Center on New York City
            var uriNewYork = new Uri(@"bingmaps:?cp=40.726966~-74.006076");


        }

        private void Setting()
        {
            gmap1.Bearing = 0;
            gmap1.CanDragMap = true;
            gmap1.DragButton = MouseButtons.Left;
            gmap1.GrayScaleMode = true;

            gmap1.MarkersEnabled = true;
            gmap1.MaxZoom = 18;
            gmap1.MinZoom = 2;

            gmap1.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            gmap1.NegativeMode = false;
            gmap1.PolygonsEnabled = true;
            gmap1.RoutesEnabled = true;
            gmap1.ShowTileGridLines = false;
            gmap1.Zoom = 5;
            gmap1.Dock = DockStyle.Fill;

            gmap1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
        }
    }
}
