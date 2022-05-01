using System;
using System.Numerics;
using System.Collections.Generic;

namespace Rdp.Terminal.Core.Server.Models.Controls
{
    class DSA
    {
        private static int p_bits = 256;
        
        private static int q_bits = 80;

        /// <summary>
        ///     a ^ r mod n
        /// </summary>
        ///
        /// <param name="a">Operand</param>
        /// <param name="r">Power</param>
        /// <param name="n">Module</param>
        ///
        /// <returns>BigInteger</returns>
        public static BigInteger fast(BigInteger a, BigInteger r, BigInteger n)
        {
            BigInteger a1 = a;
            BigInteger z1 = r;
            BigInteger x = 1;
            
            while (z1 != 0)
            {
                while (z1 % 2 == 0)
                {
                    z1 /= 2;
                    a1 = (a1 * a1) % n;
                }
                z1 -= 1;
                x = (x * a1) % n;
            }

            return x;
        }

        /// <summary>
        ///     Generate key
        /// </summary>
        ///
        /// <returns>BigInteger[]</returns>
        public static BigInteger[] getKey()
        {
            var res = new BigInteger[5];
            Random rand = new Random();

            BigInteger q = BigInteger.genPseudoPrime(q_bits, 5, rand);
            BigInteger t = 1 << (p_bits - q_bits);
            BigInteger p = q * t + 1;
            
            while (!p.isProbablePrime(5))
            {
                q = BigInteger.genPseudoPrime(q_bits, 5, rand);
                p = q * t + 1;
            }
            
            res[0] = p;
            res[1] = q;

            BigInteger h = new BigInteger();
            while (true)
            {
                h.genRandomBits(res[0].bitCount() - 1, rand);
                res[2] = fast(h, (res[0] - 1) / res[1], res[0]);
                if (res[2] >= 1) break;
            }

            BigInteger x = new BigInteger();
            x.genRandomBits(res[1].bitCount() - 1, rand);
            res[3] = x;
            res[4] = fast(res[2], x, res[0]);

            return res;
        }
    }
}
