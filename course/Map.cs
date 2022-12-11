using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using GMap.NET.WindowsForms;
using GMap.NET;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using GMapRoute = GMap.NET.WindowsForms.GMapRoute;
using System;

namespace course
{
    public partial class Map : Form
    {
        // хардкод - это плохо!!!
        private const double FromX = 55.994474805136846;
        private const double FromY = 92.79788544574697;

        // хардкод это очень плохо!!!
        private const string ToMira81 = "55.994723, 92.796661\r\n55.994840, 92.793333\r\n55.996515, 92.793354\r\n56.000367, 92.799641\r\n56.005712, 92.801758\r\n56.005916, 92.806071\r\n55.999917, 92.806865\r\n55.995669, 92.805084\r\n55.994061, 92.804097\r\n55.992009, 92.806264\r\n56.002665, 92.848059\r\n56.004285, 92.849325\r\n56.004405, 92.849668\r\n56.006528, 92.859431\r\n56.011369, 92.858809";
        private const string ToSeverniy19 = "55.994772, 92.796614\r\n55.994802, 92.793396\r\n55.996560, 92.793240\r\n56.000762, 92.799807\r\n56.003966, 92.801738\r\n56.005789, 92.801666\r\n56.005917, 92.806017\r\n56.000003, 92.806863\r\n55.996979, 92.805189\r\n55.995248, 92.805055\r\n55.994061, 92.804272\r\n55.991967, 92.806321\r\n56.002805, 92.848354\r\n56.004709, 92.850177\r\n56.006162, 92.859091\r\n56.006928, 92.860823\r\n56.007757, 92.880570\r\n56.010602, 92.892046\r\n56.011550, 92.891937\r\n56.011732, 92.896160\r\n56.017198, 92.896809\r\n56.021716, 92.900995\r\n56.022563, 92.904243\r\n56.024741, 92.905073\r\n56.035648, 92.927825\r\n56.036353, 92.930784\r\n56.036011, 92.933130\r\n56.031151, 92.939734\r\n56.023771, 92.942982\r\n56.016470, 92.955035\r\n56.014775, 92.956695\r\n56.016911, 92.965212\r\n56.017199, 92.965092";

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

        private List<PointLatLng> ConvertToPointList(string points)
        {
            return points
                .Split('\r')
                .Select(r =>
                new PointLatLng(Convert.ToDouble(
                    r.Split(' ')[0].Trim()
                    .Remove(r.Split(' ')[0].Trim().Length - 1).Replace('.', ',')), 
                Convert.ToDouble(
                    r.Split(' ')[1].Trim()
                    .Replace('.', ','))
                )).ToList();
        }

        private void DrawMap()
        {
            GMapOverlay routes = new GMapOverlay("routes"); //Создаем объект наложения (Overlay)
            List<PointLatLng> points = new List<PointLatLng>(); //Создаем лист, где будут наши точки пути.

            PointLatLng start = new PointLatLng(FromX, FromY);
            PointLatLng end = new PointLatLng(TargetX, TargetY);
            points.Add(start);

            // magic of hardcode!
            if (TargetX == Convert.ToDouble("56,01142414341906") && TargetY == Convert.ToDouble("92,86212445763783"))
            {
                ConvertToPointList(ToMira81).ForEach(a => points.Add(a));
            }
            else if (TargetX == Convert.ToDouble("56,06762798811222") && TargetY == Convert.ToDouble("92,92319475451987"))
            {
                ConvertToPointList(ToSeverniy19).ForEach(a => points.Add(a));
            }

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

            gmap1.Position = new PointLatLng(55.994517, 92.798501); // точка в центре карты при открытии 
        }

    }
}
