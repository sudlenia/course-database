﻿using GMap.NET.WindowsForms.Markers;
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
        private const double FromX = 55.994474805136846;
        private const double FromY = 92.79788544574697;

        //55.994179, 92.797480\r\n55.994185, 92.796825\r\n55.994266, 92.796670\r\n55.994749, 92.796659\r\n55.994752, 92.796305\r\n55.994788, 92.794953\r\n55.994808, 92.793283\r\n55.995451, 92.793224\r\n55.996585, 92.793230\r\n
        private const string ToMira81 = "55.994179, 92.797480\r\n55.994185, 92.796825\r\n55.994266, 92.796670\r\n55.994749, 92.796659\r\n55.994752, 92.796305\r\n55.994788, 92.794953\r\n55.994808, 92.793283\r\n55.995451, 92.793224\r\n55.996585, 92.793230\r\n55.996711, 92.793704\r\n55.997347, 92.795166\r\n55.997624, 92.795716\r\n55.998169, 92.796871\r\n56.000324, 92.799496\r\n56.002503, 92.801021\r\n56.003769, 92.801698\r\n56.004057, 92.801743\r\n56.004611, 92.801734\r\n56.005792, 92.801616\r\n56.005888, 92.804043\r\n56.005935, 92.806056\r\n56.004992, 92.806128\r\n56.004719, 92.806228\r\n56.004144, 92.806300\r\n56.004139, 92.806309\r\n56.004008, 92.806408\r\n56.003953, 92.806498\r\n56.000260, 92.806803\r\n55.999408, 92.80672\r\n55.999135, 92.806614\r\n55.997571, 92.805621\r\n55.996845, 92.805215\r\n55.996562, 92.805089\r\n55.996330, 92.805062\r\n55.996073, 92.805026\r\n55.996091, 92.805037\r\n55.995806, 92.805069\r\n55.995709, 92.805050\r\n55.995609, 92.805037\r\n55.995327, 92.805057\r\n55.995156, 92.805031\r\n55.994878, 92.804942\r\n55.994460, 92.804559\r\n55.994207, 92.804368\r\n55.994125, 92.804304\r\n55.993972, 92.804259\r\n55.993872, 92.804240\r\n55.993658, 92.804310\r\n55.993486, 92.804457\r\n55.992313, 92.805637\r\n55.992120, 92.805924\r\n55.991991, 92.806364\r\n55.991942, 92.806715\r\n55.991934, 92.806964\r\n55.991952, 92.807162\r\n55.992009, 92.807653\r\n55.992277, 92.809082\r\n55.992812, 92.812246\r\n55.992887, 92.812482\r\n55.993340, 92.814262\r\n55.993519, 92.815264\r\n55.993932, 92.81805\r\n55.994150, 92.819429\r\n55.994514, 92.821688\r\n55.995870, 92.825617\r\n55.997856, 92.831229\r\n55.998110, 92.832033\r\n55.999639, 92.836723\r\n56.000573, 92.840206\r\n56.000869, 92.841265\r\n56.001226, 92.842796\r\n56.002061, 92.846401\r\n56.002225, 92.847058\r\n56.002350, 92.847428\r\n56.002610, 92.847925\r\n56.003060, 92.848436\r\n56.003976, 92.848978\r\n56.004518, 92.849731\r\n56.004889, 92.850726\r\n56.005243, 92.852308\r\n56.005478, 92.853578\r\n56.005977, 92.856614\r\n56.006099, 92.857488\r\n56.006245, 92.858917\r\n56.006359, 92.859294\r\n56.006484, 92.859434\r\n56.006762, 92.859440\r\n56.007778, 92.859325\r\n56.008071, 92.859268\r\n56.008577, 92.859089\r\n56.009854, 92.858834\r\n56.011388, 92.858668\r\n56.011516, 92.860773\r\n56.011595, 92.862036";
        private const string ToKarla135 = "55.994179, 92.797480\r\n55.994185, 92.796825\r\n55.994266, 92.796670\r\n55.994749, 92.796659\r\n55.994752, 92.796305\r\n55.994788, 92.794953\r\n55.994808, 92.793283\r\n55.995451, 92.793224\r\n55.996585, 92.793230\r\n55.996711, 92.793704\r\n55.997347, 92.795166\r\n55.997624, 92.795716\r\n55.998169, 92.796871\r\n56.000324, 92.799496\r\n56.002503, 92.801021\r\n56.003769, 92.801698\r\n56.004057, 92.801743\r\n56.004611, 92.801734\r\n56.005792, 92.801616\r\n56.005888, 92.804043\r\n56.005935, 92.806056\r\n56.004992, 92.806128\r\n56.004719, 92.806228\r\n56.004144, 92.806300\r\n56.004139, 92.806309\r\n56.004008, 92.806408\r\n56.003953, 92.806498\r\n56.000260, 92.806803\r\n55.999408, 92.80672\r\n55.999135, 92.806614\r\n55.997571, 92.805621\r\n55.996845, 92.805215\r\n55.996562, 92.805089\r\n55.996330, 92.805062\r\n55.996073, 92.805026\r\n55.996091, 92.805037\r\n55.995806, 92.805069\r\n55.995709, 92.805050\r\n55.995609, 92.805037\r\n55.995327, 92.805057\r\n55.995156, 92.805031\r\n55.994878, 92.804942\r\n55.994460, 92.804559\r\n55.994207, 92.804368\r\n55.994125, 92.804304\r\n55.993972, 92.804259\r\n55.993872, 92.804240\r\n55.993658, 92.804310\r\n55.993486, 92.804457\r\n55.992313, 92.805637\r\n55.992120, 92.805924\r\n55.991991, 92.806364\r\n55.991942, 92.806715\r\n55.991934, 92.806964\r\n55.991952, 92.807162\r\n55.992009, 92.807653\r\n55.992277, 92.809082\r\n55.992812, 92.812246\r\n55.992887, 92.812482\r\n55.993340, 92.814262\r\n55.993519, 92.815264\r\n55.993932, 92.81805\r\n55.994150, 92.819429\r\n55.994514, 92.821688\r\n55.995870, 92.825617\r\n55.997856, 92.831229\r\n55.998110, 92.832033\r\n55.999639, 92.836723\r\n56.000573, 92.840206\r\n56.000869, 92.841265\r\n56.001226, 92.842796\r\n56.002061, 92.846401\r\n56.002225, 92.847058\r\n56.002350, 92.847428\r\n56.002610, 92.847925\r\n56.003060, 92.848436\r\n56.003976, 92.848978\r\n56.004518, 92.849731\r\n56.004889, 92.850726\r\n56.005243, 92.852308\r\n56.005478, 92.853578\r\n56.005977, 92.856614\r\n56.006099, 92.857488\r\n56.006245, 92.858917\r\n56.006359, 92.859294\r\n56.006484, 92.859434\r\n56.006762, 92.859440\r\n56.007778, 92.859325\r\n56.008071, 92.859268\r\n56.009009, 92.859021\r\n56.009017, 92.859584\r\n56.009422, 92.859533\r\n56.009503, 92.861585\r\n56.009651, 92.861564";

        private const string ToMira126 = "55.994179, 92.797480\r\n55.994185, 92.796825\r\n55.994266, 92.796670\r\n55.994749, 92.796659\r\n55.994752, 92.796305\r\n55.994788, 92.794953\r\n55.994808, 92.793283\r\n55.995451, 92.793224\r\n55.996585, 92.793230\r\n55.996693, 92.793682\r\n55.997279, 92.795053\r\n55.997669, 92.795782\r\n55.998221, 92.796942\r\n56.000341, 92.799545\r\n56.000858, 92.799932\r\n56.002560, 92.801070\r\n56.003718, 92.801677\r\n56.004027, 92.801737\r\n56.004574, 92.801737\r\n56.007811, 92.801486\r\n56.009274, 92.801342\r\n56.009393, 92.801365\r\n56.010364, 92.801304\r\n56.010962, 92.801228\r\n56.011085, 92.801623\r\n56.011238, 92.802556\r\n56.011276, 92.802731\r\n56.011280, 92.803679\r\n56.011310, 92.804794\r\n56.011446, 92.807070\r\n56.011454, 92.807252\r\n56.011620, 92.808853\r\n56.011738, 92.809907\r\n56.011794, 92.810507\r\n56.011823, 92.811091\r\n56.011823, 92.811713\r\n56.011823, 92.811956\r\n56.011611, 92.813481\r\n56.011620, 92.815552\r\n56.011669, 92.817530\r\n56.011712, 92.818030\r\n56.011856, 92.818615\r\n56.012293, 92.819677\r\n56.012411, 92.820041\r\n56.012467, 92.820428\r\n56.012500, 92.821164\r\n56.012492, 92.823159\r\n56.012462, 92.824570\r\n56.012441, 92.825533\r\n56.012539, 92.829190\r\n56.012551, 92.830032\r\n56.012496, 92.831709\r\n56.012483, 92.832169\r\n56.012391, 92.833802\r\n56.012312, 92.834134\r\n56.012227, 92.835027\r\n56.012212, 92.835754\r\n56.012127, 92.836267\r\n56.011895, 92.836619\r\n56.011678, 92.836691\r\n56.010266, 92.837007\r\n56.010498, 92.841536\r\n56.010907, 92.841463\r\n56.010947, 92.842281\r\n56.010904, 92.842287\r\n56.010886, 92.842329\r\n56.010918, 92.843021";
        private const string ToAdha109 = "55.994179, 92.797480\r\n55.994185, 92.796825\r\n55.994266, 92.796670\r\n55.994749, 92.796659\r\n55.994752, 92.796305\r\n55.994788, 92.794953\r\n55.994808, 92.793283\r\n55.995451, 92.793224\r\n55.996585, 92.793230\r\n55.996693, 92.793682\r\n55.997279, 92.795053\r\n55.997669, 92.795782\r\n55.998221, 92.796942\r\n56.000341, 92.799545\r\n56.000858, 92.799932\r\n56.002560, 92.801070\r\n56.003718, 92.801677\r\n56.004027, 92.801737\r\n56.004574, 92.801737\r\n56.007811, 92.801486\r\n56.009274, 92.801342\r\n56.009393, 92.801365\r\n56.010364, 92.801304\r\n56.010962, 92.801228\r\n56.011085, 92.801623\r\n56.011238, 92.802556\r\n56.011276, 92.802731\r\n56.011280, 92.803679\r\n56.011310, 92.804794\r\n56.011446, 92.807070\r\n56.011454, 92.807252\r\n56.011620, 92.808853\r\n56.011738, 92.809907\r\n56.011794, 92.810507\r\n56.011823, 92.811091\r\n56.011823, 92.811713\r\n56.011823, 92.811956\r\n56.011611, 92.813481\r\n56.011620, 92.815552\r\n56.011669, 92.817530\r\n56.011712, 92.818030\r\n56.011856, 92.818615\r\n56.012293, 92.819677\r\n56.012411, 92.820041\r\n56.012467, 92.820428\r\n56.012500, 92.821164\r\n56.012492, 92.823159\r\n56.012462, 92.824570\r\n56.012441, 92.825533\r\n56.012539, 92.829190\r\n56.012551, 92.830032\r\n56.012496, 92.831709\r\n56.012494, 92.832295\r\n56.012316, 92.834873\r\n56.012287, 92.835715\r\n56.012522, 92.839492\r\n56.012601, 92.839951\r\n56.012786, 92.840716\r\n56.012958, 92.840920\r\n56.013229, 92.841023\r\n56.014619, 92.840844\r\n56.014940, 92.840742\r\n56.015411, 92.840908\r\n56.015497, 92.840997\r\n56.015732, 92.841329\r\n56.015789, 92.841329\r\n56.016188, 92.843319\r\n56.016217, 92.843842\r\n56.016395, 92.847351\r\n56.016017, 92.847440\r\n56.015169, 92.847632\r\n56.013371, 92.848001\r\n56.013514, 92.851000\r\n56.013835, 92.850962\r\n56.013814, 92.850438\r\n56.014323, 92.850401\r\n56.014342, 92.850445\r\n56.014346, 92.850527\r\n56.014486, 92.850506\r\n56.014497, 92.850341\r\n56.014499, 92.850310\r\n56.014524, 92.850282\r\n56.014571, 92.850270\r\n56.014640, 92.850242\r\n56.014773, 92.850087\r\n56.014878, 92.849922\r\n56.014922, 92.849889\r\n56.015084, 92.849885\r\n56.015091, 92.850424";

        private const string ToSvobodny76 = "55.994179, 92.797480\r\n55.994185, 92.796825\r\n55.994266, 92.796670\r\n55.994749, 92.796659\r\n55.994752, 92.796305\r\n55.994788, 92.794953\r\n55.994808, 92.793283\r\n55.995451, 92.793224\r\n55.996585, 92.793230\r\n55.996477, 92.791669\r\n55.996378, 92.790564\r\n55.996153, 92.789110\r\n55.996075, 92.787892\r\n55.995937, 92.785897\r\n55.995445, 92.782581\r\n55.994892, 92.779390\r\n55.994841, 92.778934\r\n55.994739, 92.777571\r\n55.994646, 92.775715\r\n55.994508, 92.773800\r\n55.994460, 92.772126\r\n55.994454, 92.770688\r\n55.994421, 92.769685\r\n55.994235, 92.768355\r\n55.994040, 92.767679\r\n55.992750, 92.764117\r\n55.992753, 92.764101\r\n55.992201, 92.761982\r\n55.991931, 92.761193\r\n55.990824, 92.759010\r\n55.990236, 92.757986\r\n55.989975, 92.757642\r\n55.989225, 92.756135\r\n55.988778, 92.754772\r\n55.988574, 92.754043\r\n55.988505, 92.753469\r\n55.988583, 92.753233\r\n55.988826, 92.752997\r\n55.989306, 92.752943\r\n55.989705, 92.752771\r\n55.990098, 92.752535\r\n55.991355, 92.751264\r\n55.991955, 92.750765\r\n55.993185, 92.749528\r\n55.993419, 92.749485\r\n55.994079, 92.749710\r\n55.994283, 92.749715\r\n55.995006, 92.750912\r\n55.996251, 92.753213\r\n55.998450, 92.757526\r\n55.999779, 92.760187\r\n56.000992, 92.762512\r\n56.002087, 92.764641\r\n56.002834, 92.766052\r\n56.002900, 92.765714\r\n56.002978, 92.765500\r\n56.003263, 92.766084\r\n56.003677, 92.765366\r\n56.003794, 92.764952\r\n56.003983, 92.764668\r\n56.005455, 92.767645\r\n56.005803, 92.768713\r\n56.006292, 92.769797\r\n56.006244, 92.770172\r\n56.006241, 92.770392\r\n56.006313, 92.770719";
        private const string ToMagazine = "55.994179, 92.797480\r\n55.994185, 92.796825\r\n55.994266, 92.796670\r\n55.994749, 92.796659\r\n55.994752, 92.796305\r\n55.994788, 92.794953\r\n55.994808, 92.793283\r\n55.995451, 92.793224\r\n55.996585, 92.793230\r\n55.996711, 92.793704\r\n55.997347, 92.795166\r\n55.997624, 92.795716\r\n55.998169, 92.796871\r\n56.000324, 92.799496\r\n56.002503, 92.801021\r\n56.003769, 92.801698\r\n56.004057, 92.801743\r\n56.004611, 92.801734\r\n56.005792, 92.801616\r\n56.005888, 92.804043\r\n56.005935, 92.806056\r\n56.004992, 92.806128\r\n56.004719, 92.806228\r\n56.004144, 92.806300\r\n56.004139, 92.806309\r\n56.004008, 92.806408\r\n56.003953, 92.806498\r\n56.000260, 92.806803\r\n55.999408, 92.80672\r\n55.999135, 92.806614\r\n55.997571, 92.805621\r\n55.996845, 92.805215\r\n55.996562, 92.805089\r\n55.996330, 92.805062\r\n55.996073, 92.805026\r\n55.995780, 92.805080\r\n55.995508, 92.805143\r\n55.995205, 92.805278\r\n55.994963, 92.805405\r\n55.993918, 92.806244\r\n55.993252, 92.806803\r\n55.992849, 92.807092\r\n55.991007, 92.807985\r\n55.977668, 92.815177\r\n55.976017, 92.815737\r\n55.975755, 92.815971\r\n55.975593, 92.816215\r\n55.975492, 92.816530\r\n55.975457, 92.816909\r\n55.975563, 92.817478\r\n55.975760, 92.817784\r\n55.976598, 92.818227\r\n55.976780, 92.818786\r\n55.976835, 92.818849\r\n55.976911, 92.821294\r\n55.976981, 92.822061\r\n55.977042, 92.822232\r\n55.977035, 92.823257\r\n55.977140, 92.826165\r\n55.977239, 92.827527\r\n55.978105, 92.835060\r\n55.978138, 92.835457\r\n55.979486, 92.847622\r\n55.979567, 92.848673\r\n55.979678, 92.849414\r\n55.979774, 92.850100\r\n55.979792, 92.850390\r\n55.979846, 92.850819\r\n55.980767, 92.859673\r\n55.980936, 92.861298\r\n55.980942, 92.861309\r\n55.981413, 92.862698\r\n55.981629, 92.863707\r\n55.983558, 92.883662\r\n55.983651, 92.884719\r\n55.983732, 92.885958\r\n55.983828, 92.887412\r\n55.983567, 92.887594\r\n55.983030, 92.887782\r\n55.982979, 92.887847\r\n55.982382, 92.888034\r\n55.982253, 92.888099\r\n55.981685, 92.888233\r\n55.981160, 92.888415\r\n55.981178, 92.888603\r\n55.981295, 92.889778\r\n55.981727, 92.892599\r\n55.982187, 92.894836\r\n55.983306, 92.901483\r\n55.983789, 92.903607\r\n55.984755, 92.907465\r\n55.985121, 92.908709\r\n55.985451, 92.910265\r\n55.985772, 92.913038\r\n55.986006, 92.914621\r\n55.986403, 92.916761\r\n55.986847, 92.918574\r\n55.987234, 92.919867\r\n55.987837, 92.922040\r\n55.989296, 92.928247\r\n55.989620, 92.929294\r\n55.990199, 92.930763\r\n55.990673, 92.932110\r\n55.991069, 92.933279\r\n55.992230, 92.938306\r\n55.992176, 92.939164\r\n55.991120, 92.939953\r\n55.990394, 92.940457\r\n55.990559, 92.941304\r\n55.990505, 92.941353";

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

        public Map(List<PointLatLng> points)
        {
            InitializeComponent();

            Setting();
            DrawMap(points);
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

        private void DrawMap(List<PointLatLng> points = null)
        {
            GMapOverlay routes = new GMapOverlay("routes"); //Создаем объект наложения (Overlay)
            GMapOverlay markersOverlay = new GMapOverlay("marker"); //Создаем Overlay

            if (points is null)
            {
                points = new List<PointLatLng>();
                PointLatLng start = new PointLatLng(FromX, FromY);
                PointLatLng end = new PointLatLng(TargetX, TargetY);
                points.Add(start);

                // magic of hardcode!
                if (TargetX == Convert.ToDouble("56,01142414341906") && TargetY == Convert.ToDouble("92,86212445763783"))
                {
                    ConvertToPointList(ToMira81).ForEach(a => points.Add(a));
                }
                else if (TargetX == Convert.ToDouble("56,00619184444418") && TargetY == Convert.ToDouble("92,7708360287757"))
                {
                    ConvertToPointList(ToSvobodny76).ForEach(a => points.Add(a));
                }
                else if (TargetX == Convert.ToDouble("56,009657449935865") && TargetY == Convert.ToDouble("92,861770901567"))
                {
                    ConvertToPointList(ToKarla135).ForEach(a => points.Add(a));
                }
                else if (TargetX == Convert.ToDouble("55,990488225723084") && TargetY == Convert.ToDouble("92,94134369144753"))
                {
                    ConvertToPointList(ToMagazine).ForEach(a => points.Add(a));
                }
                else if (TargetX == Convert.ToDouble("56,01079310426668") && TargetY == Convert.ToDouble("92,8430333408457"))
                {
                    ConvertToPointList(ToMira126).ForEach(a => points.Add(a));
                }
                else if (TargetX == Convert.ToDouble("56,01507910034775") && TargetY == Convert.ToDouble("92,85036576453813"))
                {
                    ConvertToPointList(ToAdha109).ForEach(a => points.Add(a));
                }
                points.Add(end);
            }
            else
            {
                points.ForEach(p =>
                {
                    GMarkerGoogle marker = new GMarkerGoogle(p, GMarkerGoogleType.black_small);
                    markersOverlay.Markers.Add(marker);
                });
            }

            //MapRoute route = GoogleMapProvider.Instance.GetRoute(start, end, false, true, 14);

            GMapRoute r = new GMapRoute(points, "Путь доставки");
            r.Stroke = new Pen(Color.Blue, 3); //Задаем цвет и ширину линии
            routes.Routes.Add(r); //Добавляем на наш Overlay маршрут
            gmap1.Overlays.Add(routes); //Накладываем Overlay на карту.

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
            gmap1.MinZoom = 13;

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
