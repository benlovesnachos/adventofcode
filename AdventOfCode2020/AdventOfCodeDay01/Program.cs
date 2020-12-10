﻿using System;

namespace AdventOfCodeDay01
{
    class Program
    {
        private static int[] testAccountingEntries = { 1721, 979, 366, 299, 675, 1456 };
        private static int[] accountingEntries = { 1310, 1960, 1530, 1453, 1572, 1355, 1314, 1543, 1439, 1280, 1367, 1368, 1313, 1423, 1771, 1868, 1555, 1635, 1200, 2009, 1649, 1824, 1979, 1523, 1548, 1415, 1371, 101, 1836, 1570, 1494, 1850, 1624, 1151, 1408, 1220, 1372, 1871, 1712, 1765, 1950, 1654, 1797, 1814, 1592, 1747, 1566, 1600, 1445, 1297, 1374, 1916, 274, 1735, 1392, 1977, 1957, 1672, 249, 1980, 1791, 1733, 1962, 1641, 1487, 1486, 1741, 1751, 1309, 1342, 1567, 1353, 1909, 1657, 1837, 1438, 1510, 1811, 1939, 1558, 1215, 2010, 1891, 1929, 1208, 1459, 1272, 1696, 1820, 1206, 1414, 1795, 1884, 1734, 1745, 421, 1908, 1986, 1329, 932, 1468, 1720, 1769, 1402, 1913, 1580, 1707, 1799, 1185, 1587, 1521, 1428, 1210, 1822, 194, 1973, 2000, 1940, 1481, 1509, 1563, 1697, 1924, 1671, 1516, 1758, 1552, 1996, 2002, 1883, 1539, 1089, 1794, 1337, 1875, 1736, 1683, 1682, 1354, 1846, 1427, 1359, 1854, 1574, 1198, 359, 1859, 1517, 1706, 1537, 1915, 1983, 1482, 1941, 1703, 1954, 1597, 1903, 1991, 53, 1515, 1259, 1421, 1384, 1948, 1776, 1965, 1625, 1478, 1629, 1949, 1144, 1951, 1656, 1137, 1349, 1910, 1483, 1229, 1480, 1324, 1664, 1604, 1764, 1564, 1673, 1686, 1841, 1640, 1627, 1984, 1258, 1376, 855, 1413, 1261, 1429, 1863, 1540, 692 };

        static void Main(string[] args)
        {
            //Program.doWork(accountingEntries);
            Program.doWorkThreeNumbers(accountingEntries);
        }

        public static void doWork(int[] checkThese)
        {
            bool entriesFound = false;
            for (int i = 0; i < checkThese.Length; i++)
            {
                for (int j = 0; j < checkThese.Length; j++)
                {
                    Console.Write("Check item {0} with item {1}... ", i, j);
                    var value = checkThese[i] + checkThese[j];
                    Console.WriteLine("sum: {0} ", value);

                    if (value == 2020)
                    {
                        entriesFound = true;
                        Console.WriteLine("product: {0} ", checkThese[i] * checkThese[j]);
                    }
                    if (entriesFound) break;
                }
                if (entriesFound) break;
            }
        }

        public static void doWorkThreeNumbers(int[] checkThese)
        {
            bool entriesFound = false;
            for (int i = 0; i < checkThese.Length; i++)
            {
                for (int j = 0; j < checkThese.Length; j++)
                {
                    for (int k = 0; k < checkThese.Length; k++)
                    {
                        var value = checkThese[i] + checkThese[j] + checkThese[k];

                        if (value == 2020)
                        {
                            entriesFound = true;
                            Console.Write("Check item {0}, item {1}, and item {2}... ", i, j, k);
                            Console.Write("sum: {0} ", value);
                            Console.WriteLine("product: {0} ", checkThese[i] * checkThese[j] * checkThese[k]);
                        }
                        if (entriesFound) break;
                    }
                    if (entriesFound) break;
                }
                if (entriesFound) break;
            }
        }
    }
}
