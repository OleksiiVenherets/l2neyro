using System;
using System.Collections.Generic;
using System.Linq;

namespace l2neyro
{
    class Calculation
    {
        public double [] GetProbability (List<Iris> list, double [] inputIris, double sigma)
        {
            double [] probabilities = new double [3];
            var setosaList = new List<Iris>();
            var versicolorList = new List<Iris>();
            var virginicaList = new List<Iris>();

            foreach (var iris in list)
            {
                switch(iris.Name)
                {
                    case "setosa":
                        setosaList.Add(iris);
                        break;
                    case "versicolor":
                        versicolorList.Add(iris);
                        break;
                    case "virginica":
                        virginicaList.Add(iris);
                        break;
                }
            }

            var setosad = SumD(setosaList, inputIris, sigma);
            var versicolord = SumD(versicolorList, inputIris, sigma);
            var virginicad = SumD(virginicaList, inputIris, sigma);

            probabilities[0] = setosad / (setosad + versicolord + virginicad);
            probabilities[1] = versicolord / (setosad + versicolord + virginicad);
            probabilities[2] = virginicad / (setosad + versicolord + virginicad);
                        
            return probabilities;
        }

        private List<double> GetR(List<Iris> list, double[] inputIris)
        {
            var rList = new List<double>();

            foreach (var iris in list)
            {
                double r = Math.Sqrt(Math.Pow((iris.SepalLength - inputIris[0]), 2) + Math.Pow((iris.SepalWidth - inputIris[1]), 2) +
                    Math.Pow((iris.PetalLength - inputIris[2]), 2) + Math.Pow((iris.PetalWidth - inputIris[3]), 2));
                rList.Add(r);
            }

            return rList;
        }

        private List<double> GetD(List<double> list, double sigma)
        {
            var dList = new List<double>();

            foreach (var item in list)
            {
                double d = Math.Exp(-Math.Pow(item, 2) / Math.Pow(sigma, 2));
                dList.Add(d);
            }

            return dList;
        }

        private double SumD (List<Iris> list, double[] inputIris, double sigma)
        {
            var rList = GetR(list, inputIris);
            var dList = GetD(rList, sigma);
            return dList.Sum();
        }
    }
}
