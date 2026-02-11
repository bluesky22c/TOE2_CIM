import os
import sys
import glob

if __name__ == '__main__':
  os.system('del *.nupkg')
  fn = glob.glob('*.csproj')
  os.system(f'nuget pack {fn[0]}')
  fn = glob.glob('*.nupkg')
  os.system(f'nuget push {fn[0]} -source EVisionLib -apikey mykey < login.txt')
#  os.system('del *.nupkg')
  print(fn) 