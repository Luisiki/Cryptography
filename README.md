# Krypto
This is a hobby project, where I try to learn and program cryptographic functions, algrotihms and different approaches for computing cryptographic functions.

Every function is designed to be able to work with BigInteger type meaning it should be able to work with really large numbers.

Everything is coded in C# on .NET versions 7.0

# Factorization
A big thing I try to focus on is factorization.
The implemented, or to be implemented, algortihms for this are:

1. Classical brute force, ๐ข
2. Pollard Rho (130 bit number succesfully factored), ๐ข
3. Baby-Step Giant-Step, ๐ข
4. Quadratic sieve. ๐ด


# Traditional cryptographic functions
Euler's totient function $\varphi(n)$:

1. Brute force i.e. try all numbers $a$ if $a|n$, where $a < n$ ๐ข
2. Mรถbius function approach $\varphi(n) = \sum(\mu(n)*\frac{n}{d})$, for all $d$ to $n$, where $d|n$ ๐  (has a 8% error rate without any reason, needs to be fixed),
3. Classical $\varphi(n) = \prod(p_i-1)\cdot p_i^{k_i-1}$, where $n = p_1^{k_1}\cdot p_2^{k_2} ... p_i^{k_i}$. ๐ด 

Primality tests:

1. Miller-Rabin primality test ๐ข

Cyclic group generator finder:

1. Classical brute force i.e. try all $g^{a} \not\equiv 1 \text{ mod }n$, where $a$ goes from $1$ to $n$, ๐ข
2. Pollard rho, use Pollard rho to get factors of $n$ to the try $g^{\frac{\varphi(n)}{p_i}} \not\equiv 1 \text{ mod }n$, ๐ 
3. Index calculator, ๐ด
4. Index candidate. ๐ด

Mรถbius function: ๐ข
$\mu(n) = $

$+1  \quad \text{if }n\text{ is a square-free positive integer with an even number of prime factors};$

$-1  \quad \text{if }n\text{ is a square-free positive integer with an odd number of prime factors};$

$0  \quad \text{if }n\text{ is not a square-free positive integer}.$

