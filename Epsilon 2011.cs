using System;
using System.Collections.Generic;

//Epsilon 2011
class Solution
{
    #region Private Structs & Classes
    //Represents cartesian coordinates in two dimensions
    private struct CartesianCoordinates
    {
        #region Properties
        public double X { get; private set; }

        public double Y { get; private set; }
        #endregion

        #region Constructor
        public CartesianCoordinates(double x, double y)
            : this()
        {
            X = x;
            Y = y;
        }
        #endregion
    }

    //Represents linear function
    private class LinearFunction : IComparable<LinearFunction>, IComparable
    {
        #region Properties
        public int Slope { get; private set; }

        public int Intercept { get; private set; }
        #endregion

        #region Constructor
        public LinearFunction(int slope, int intercept)
        {
            Slope = slope;
            Intercept = intercept;
        }
        #endregion

        #region Methods
        public double Evaluate(double x)
        {
            return Slope * x + Intercept;
        }

        public CartesianCoordinates? Intersect(LinearFunction other)
        {
            if (this.Slope == other.Slope)
                return null;

            double x = (double)(this.Intercept - other.Intercept) / (double)(other.Slope - this.Slope);
            return new CartesianCoordinates(x, this.Evaluate(x));
        }
        #endregion

        #region IComparable Members
        public int CompareTo(LinearFunction other)
        {
            //If slope and intercept are equal we treat the functions as equal
            if (this.Slope == other.Slope && this.Intercept == other.Intercept)
                return 0;
            //Functions with smaller slope first, if slope is equal the larger intercept goes first
            else if (this.Slope < other.Slope || (this.Slope == other.Slope && this.Intercept > other.Intercept))
                return -1;
            else
                return 1;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (!(obj is LinearFunction))
                throw new ArgumentException("Object must be of type LinearFunction", "obj");

            return CompareTo((LinearFunction)obj);
        }
        #endregion
    }

    //Represents envelope for collection of linear functions (envelope consist of lines and intersection points at which those lines connect)
    private class Envelope
    {
        #region Properties
        public LinearFunction[] Lines { get; private set; }

        public CartesianCoordinates[] Intersections { get; private set; }
        #endregion

        #region Constructor
        public Envelope(LinearFunction[] lines, CartesianCoordinates[] intersections)
        {
            Lines = lines;
            Intersections = intersections;
        }
        #endregion
    }
    #endregion

    #region Private Enums
    private enum EnvelopeKind
    {
        Upper,
        Lower
    }
    #endregion

    #region Fields
    private int _leftBorder = -10000000;
    private int _rightBorder = 10000000;
    #endregion

    #region Public Methods
    public double solution(int[] A, int[] B)
    {
        //This is where result will be stored
        double result = Double.MaxValue;

        //Sanity check
        if (A != null && A.Length > 0 && B != null && B.Length > 0)
        {
            int N = A.Length;

            //We will treat every A[K]*X+B as linear function
            LinearFunction[] functions = new LinearFunction[N];
            for (int i = 0; i < N; i++)
                functions[i] = new LinearFunction(A[i], B[i]);

            //We sort the functions by slope and intercept
            Array.Sort<LinearFunction>(functions);

            //We get the upper and lower envelopes for the functions collection
            Envelope upperEnvelope = GetEnvelope(functions, EnvelopeKind.Upper);
            Envelope lowerEnvelope = GetEnvelope(functions, EnvelopeKind.Lower);

            int upperEnvelopePointIndex = 1;
            int lowerEnvelopePointIndex = 1;

            //We need to minimaze the distance between envelopes, we will start by going through upper envelope
            while (upperEnvelopePointIndex < upperEnvelope.Intersections.Length)
            {
                double distance = Double.MaxValue;
                    
                //If current intersection in upper envelope lies before the current intersection in lower envelope (or there are no more intersections in lower envelope)
                if (upperEnvelope.Intersections[upperEnvelopePointIndex].X < lowerEnvelope.Intersections[lowerEnvelopePointIndex].X || lowerEnvelopePointIndex == lowerEnvelope.Intersections.Length - 1)
                {
                    //The distance is equal to value at current intersection from upper envelope minus the value of current function from lower envelope in this point 
                    distance = upperEnvelope.Intersections[upperEnvelopePointIndex].Y - lowerEnvelope.Lines[lowerEnvelopePointIndex - 1].Evaluate(upperEnvelope.Intersections[upperEnvelopePointIndex].X);

                    //We move to next intersection in upper envelope
                    upperEnvelopePointIndex++;
                }
                //Otherwise (the current intersection in upper envelope lies further than current intersection in lower envelope and there are still other intersection in lower envelope)
                else
                {
                    //The distance is equal to the value of current function from upper envelope in this point minus value at current intersection from lower envelope
                    distance = upperEnvelope.Lines[upperEnvelopePointIndex - 1].Evaluate(lowerEnvelope.Intersections[lowerEnvelopePointIndex].X) - lowerEnvelope.Intersections[lowerEnvelopePointIndex].Y;

                    //We move to next intersection in lower envelope
                    lowerEnvelopePointIndex++;
                }

                //If new distance is smaller than result which we laready have
                if (distance < result)
                    //We update the result
                    result = distance;
            }
        }

        //Return the result
        return result;
    }
    #endregion

    #region Private methods
    private Envelope GetEnvelope(LinearFunction[] functions, EnvelopeKind kind)
    {
        Envelope envelope = null;

        if (functions != null && functions.Length > 0)
        {
            int firstFunctionIndex, lastFunctionIndex, functionsIterationStep;
            //While looking for upper envelope we will go from first to last function
            if (kind == EnvelopeKind.Upper)
            {
                firstFunctionIndex = 0;
                lastFunctionIndex = functions.Length - 1;
                functionsIterationStep = 1;
            }
            //While looking for lower envelope we will go from last to first function
            else
            {
                firstFunctionIndex = functions.Length - 1;
                lastFunctionIndex = 0;
                functionsIterationStep = -1;
            }

            List<LinearFunction> lines = new List<LinearFunction>();
            List<CartesianCoordinates> points = new List<CartesianCoordinates>();

            //We set the first line in the envelope
            LinearFunction previousLine = functions[firstFunctionIndex];
            lines.Add(previousLine);

            //And calculate "theoretical" furthest to left point
            points.Add(new CartesianCoordinates(_leftBorder, previousLine.Evaluate(_leftBorder)));

            //We will look for intersection points between the functions
            for (int i = firstFunctionIndex + functionsIterationStep; (kind == EnvelopeKind.Upper && i <= lastFunctionIndex) || (kind == EnvelopeKind.Lower && i >= lastFunctionIndex); i += functionsIterationStep)
            {
                LinearFunction currentLine = functions[i];
                //Because we have taken intercept into consideration while sorting we can skip parallel lines
                if (currentLine.Slope != previousLine.Slope)
                {
                    //We add an intersection between previous and current line to the points collection
                    points.Add(previousLine.Intersect(currentLine).Value);

                    //If the latest intersection point lies before any of the previous one, we need to update those intersections to find the actual last intersection
                    int lastIntersectionIndex = points.Count - 1;
                    while (points[lastIntersectionIndex].X < points[lastIntersectionIndex - 1].X)
                    {
                        //Calculate the intersection between current on previous line
                        points[lastIntersectionIndex - 1] = lines[lastIntersectionIndex - 2].Intersect(currentLine).Value;

                        //Remove the no longer valid intersection and line
                        points.RemoveAt(lastIntersectionIndex);
                        lines.RemoveAt(lastIntersectionIndex - 1);

                        lastIntersectionIndex--;
                    }

                    //We add current line to the lines collection
                    previousLine = currentLine;
                    lines.Add(previousLine);
                }
            }

            //We calculate "theoretical" furthest to right point
            points.Add(new CartesianCoordinates(_rightBorder, previousLine.Evaluate(_rightBorder)));

            envelope = new Envelope(lines.ToArray(), points.ToArray());
        }

        return envelope;
    }
    #endregion
}
