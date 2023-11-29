#!/usr/bin/env python3
import subprocess

IN = 'IN'
OUT = 'OUT'
TODIR = 'out'

UU = ['0', '1']  # FF0000
UC = '#ff0000'
LL = ['0', '1']  # FFFF00
LC = '#ffff00'
RR = ['0', '1']  # 00FF00
RC = '#00ff00'
DD = ['0', '1']  # 0000FF
DC = '#0000ff'

VV = ['#ffffff', '#000000']

SZ = [16, 32, 48, 64, 128, 256]

def xf(idx, oc):
    return f'-fill {VV[idx]} -opaque {oc}'

def tocmd(arg, str, sz):
    return f'convert {arg} -transparent white {IN}{sz}.png -define icon:auto-resize={sz} {TODIR}/{str}/{OUT}{sz}.ico'.split()

for u in enumerate(UU):
    uf = xf(u[0], UC)
    for l in enumerate(LL):
        lf = xf(l[0], LC)
        for r in enumerate(RR):
            rf = xf(r[0], RC)
            for d in enumerate(DD):
                df = xf(d[0], DC)
                str = f'{u[1]}{l[1]}{r[1]}{d[1]}'
                arg = f'{uf} {lf} {rf} {df}'
                subprocess.call(['mkdir', '-p', f'{TODIR}/{str}'])
                imgs = ['convert']
                for sz in SZ:
                    subprocess.call(tocmd(arg, str, sz))
                    imgs.append(f'{TODIR}/{str}/{OUT}{sz}.ico')
                imgs.append(f'{TODIR}/{OUT}_{str}.ico')
                subprocess.call(imgs)

