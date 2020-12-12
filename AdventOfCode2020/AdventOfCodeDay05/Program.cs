using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AdventOfCodeDay05
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] fileList = { "data/TestFile.txt", "data/LiveFile.txt" };

            foreach (var file in fileList)
            {
                Program pgm = new Program(file);
            }
        }


        public string fileName { get; set; } = "";
        public int validCount { get; set; } = 0;



        public Program(string fileName)
        {

            this.fileName = fileName;
            var maxSeatNum = 0;
            var minSeatNum = 1024;

            List<Seat> seats = new List<Seat>();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            string[] lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var seat = new Seat(line);
                if (seat.seatId > maxSeatNum) maxSeatNum = seat.seatId;
                if (seat.seatId < minSeatNum) minSeatNum = seat.seatId;
                seats.Add(seat);
                //Console.WriteLine($"Seat : {seat}");
            }

            // Sort ArrayList.
            seats.Sort();

            Console.WriteLine($"{this.fileName} maxSeat {maxSeatNum} minSeat {minSeatNum}");

            for (var i = 0; i < seats.Count;i++ ) {
                if ((minSeatNum + i) != seats[i].seatId) {
                    Console.WriteLine($"Seat is between {seats[i - 1]} {seats[i]}");
                    Console.WriteLine($"My seat is {seats[i - 1].seatId + 1}");
                    break;
                }
            }

        }
    }

    class Seat : IComparable
    {

        public string boardingPass { get; set; } = "";
        public int row { get; set; } = 0;
        public int column { get; set; } = 0;
        public int seatId { get { return row * 8 + column; } }


        public Seat(string boardingPass)
        {
            this.boardingPass = boardingPass;
            var rowRange = new RowRange(0, 127);
            var colRange = new RowRange(0, 7);

            //Console.WriteLine($"rowRange {rowRange}");


            //var ticketStub = "FBFBBFFRLR";

            for (int i = 0; i < boardingPass.Length; i++)
            {
                if (i < 6)
                {
                    rowRange = narrowRange(rowRange, (boardingPass[i].ToString() == "B"));
                }
                else if (i == 6)
                {
                    if (boardingPass[i].ToString() == "B")
                    {
                        row = rowRange.high;
                    }
                    else
                    {
                        row = rowRange.low;
                    }
                }
                else if (i < boardingPass.Length - 1)
                {
                    colRange = narrowRange(colRange, (boardingPass[i].ToString() == "R"));
                }
                else
                {
                    if (boardingPass[i].ToString() == "R")
                    {
                        column = colRange.high;
                    }
                    else
                    {
                        column = colRange.low;
                    }
                }
            }
        }

        public int CompareTo(object obj) {
            if (obj == null) return 1;
            Seat otherSeat = obj as Seat;
            if (otherSeat != null)
                return this.seatId.CompareTo(otherSeat.seatId);
            else
                throw new ArgumentException("Object is not a Seat");
        }

        private RowRange narrowRange(RowRange rowRange, bool high)
        {
            //Console.WriteLine($"narrowRange {rowRange} {high}");

            var returnValue = new RowRange();
            var middle = ((rowRange.high - rowRange.low) / 2);
            if (high)
            {
                returnValue.high = rowRange.high;
                returnValue.low = rowRange.high - middle;
            }
            else
            {
                returnValue.high = rowRange.low + middle;
                returnValue.low = rowRange.low;
            }
            //Console.WriteLine($"narrowed range {returnValue}");
            return returnValue;
        }




        override
        public string ToString()
        {
            return $"row {row}, column {column}, seat ID {seatId}";
        }
    }

    class RowRange
    {
        public int low { get; set; } = 0;
        public int high { get; set; } = 0;
        public RowRange() { }

        public RowRange(int low, int high)
        {
            this.low = low;
            this.high = high;
        }

        override
        public string ToString()
        {
            return $"low {low} high {high}";
        }


    }
}
