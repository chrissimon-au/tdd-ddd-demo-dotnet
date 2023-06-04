{ pkgs, ... }: 

{
  languages.dotnet.enable = true;
  packages = [ pkgs.watchexec ];
  enterShell = ''
    unset DEVELOPER_DIR
  '';
}
