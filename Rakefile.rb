require "rubygems"
require "bundler"
Bundler.setup
$: << './'

require 'albacore'
require 'rake/clean'
require 'semver'

require 'buildscripts/utils'
require 'buildscripts/paths'
require 'buildscripts/project_details'
require 'buildscripts/environment'

# to get the current version of the project, type 'SemVer.find.to_s' in this rake file.

desc 'generate the shared assembly info'
assemblyinfo :assemblyinfo => ["env:release"] do |asm|
  data = commit_data() #hash + date
  asm.product_name = asm.title = PROJECTS[:app][:title]
  asm.description = PROJECTS[:app][:description] + " #{data[0]} - #{data[1]}"
  asm.company_name = PROJECTS[:app][:company]
  # This is the version number used by framework during build and at runtime to locate, link and load the assemblies. When you add reference to any assembly in your project, it is this version number which gets embedded.
  asm.version = BUILD_VERSION
  # Assembly File Version : This is the version number given to file as in file system. It is displayed by Windows Explorer. Its never used by .NET framework or runtime for referencing.
  asm.file_version = BUILD_VERSION
  asm.custom_attributes :AssemblyInformationalVersion => "#{BUILD_VERSION}", # disposed as product version in explorer
    :CLSCompliantAttribute => false,
    :AssemblyConfiguration => "#{CONFIGURATION}",
    :Guid => PROJECTS[:app][:guid]
  asm.com_visible = false
  asm.copyright = PROJECTS[:app][:copyright]
  asm.output_file = File.join(FOLDERS[:src], 'SharedAssemblyInfo.cs')
  asm.namespaces = "System", "System.Reflection", "System.Runtime.InteropServices", "System.Security"
end

task :ensure_packages do
  Dir.glob("./src/**/packages.config") do |cfg|
    sh %Q[tools/NuGet.exe install "#{cfg}" -o "src/packages"] do |ok, res|
      puts (res.inspect) unless ok
    end
  end
end

task :ensure_modules do
  `git submodule init`
end

desc "build sln file"
msbuild :msbuild => [:assemblyinfo, :ensure_packages] do |msb|
  msb.solution   = FILES[:sln]
  msb.properties :Configuration => CONFIGURATION
  msb.targets    :Clean, :Build
  msb.verbosity = "minimal"
end


task :app_output => [:msbuild] do
  target = File.join(FOLDERS[:binaries], PROJECTS[:app][:id])
  copy_files FOLDERS[:app][:out], "*.{xml,dll,pdb,config}", target
  CLEAN.include(target)
end


task :msg_output => [:msbuild] do
  target = File.join(FOLDERS[:binaries], PROJECTS[:msg][:id])
  copy_files FOLDERS[:msg][:out], "*.{xml,dll,pdb,config}", target
  CLEAN.include(target)
end


task :domain_output => [:msbuild] do
  target = File.join(FOLDERS[:binaries], PROJECTS[:domain][:id])
  copy_files FOLDERS[:domain][:out], "*.{xml,dll,pdb,config}", target
  CLEAN.include(target)
end


task :domain_svc_output => [:msbuild] do
  target = File.join(FOLDERS[:binaries], PROJECTS[:domain_svc][:id])
  copy_files FOLDERS[:domain_svc][:out], "*.{xml,dll,pdb,config}", target
  CLEAN.include(target)
end


task :index_output => [:msbuild] do
  target = File.join(FOLDERS[:binaries], PROJECTS[:index][:id])
  copy_files FOLDERS[:index][:out], "*.{xml,dll,pdb,config}", target
  CLEAN.include(target)
end


task :indexer_tests_output => [:msbuild] do
  target = File.join(FOLDERS[:binaries], PROJECTS[:indexer_tests][:id])
  copy_files FOLDERS[:indexer_tests][:out], "*.{xml,dll,pdb,config}", target
  CLEAN.include(target)
end


task :infr_output => [:msbuild] do
  target = File.join(FOLDERS[:binaries], PROJECTS[:infr][:id])
  copy_files FOLDERS[:infr][:out], "*.{xml,dll,pdb,config}", target
  CLEAN.include(target)
end


task :rm_output => [:msbuild] do
  target = File.join(FOLDERS[:binaries], PROJECTS[:rm][:id])
  copy_files FOLDERS[:rm][:out], "*.{xml,dll,pdb,config}", target
  CLEAN.include(target)
end


task :specs_output => [:msbuild] do
  target = File.join(FOLDERS[:binaries], PROJECTS[:specs][:id])
  copy_files FOLDERS[:specs][:out], "*.{xml,dll,pdb,config}", target
  CLEAN.include(target)
end


task :wpf_output => [:msbuild] do
  target = File.join(FOLDERS[:binaries], PROJECTS[:wpf][:id])
  copy_files FOLDERS[:wpf][:out], "*.{xml,dll,pdb,config}", target
  CLEAN.include(target)
end


task :evtlist_output => [:msbuild] do
  target = File.join(FOLDERS[:binaries], PROJECTS[:evtlist][:id])
  copy_files FOLDERS[:evtlist][:out], "*.{xml,dll,pdb,config}", target
  CLEAN.include(target)
end

task :output => [:app_output, :msg_output, :domain_output, :domain_svc_output, :index_output, :indexer_tests_output, :infr_output, :rm_output, :specs_output, :wpf_output, :evtlist_output]
task :nuspecs => [:msg_nuspec]

desc "Create a nuspec for 'Documently.Messages'"
nuspec :msg_nuspec do |nuspec|
  nuspec.id = "#{PROJECTS[:msg][:nuget_key]}"
  nuspec.version = BUILD_VERSION
  nuspec.authors = "#{PROJECTS[:msg][:authors]}"
  nuspec.description = "#{PROJECTS[:msg][:description]}"
  nuspec.title = "#{PROJECTS[:msg][:title]}"
  # nuspec.projectUrl = 'http://github.com/haf' # TODO: Set this for nuget generation
  nuspec.language = "en-US"
  nuspec.licenseUrl = "http://www.apache.org/licenses/LICENSE-2.0" # TODO: set this for nuget generation
  nuspec.requireLicenseAcceptance = "false"
  
  nuspec.output_file = FILES[:msg][:nuspec]
  nuspec_copy(:msg, "#{PROJECTS[:msg][:id]}.{dll,pdb,xml}")
end

task :nugets => [:"env:release", :nuspecs, :msg_nuget]

desc "nuget pack 'Documently.Commands'"
nugetpack :msg_nuget do |nuget|
  nuget.command     = "#{COMMANDS[:nuget]}"
  nuget.nuspec      = "#{FILES[:msg][:nuspec]}"
  nuget.output      = "#{FOLDERS[:nuget]}"
end

task :publish => [:"env:release", :msg_nuget_push]

desc "publishes (pushes) the nuget package 'Documently.Commands'"
nugetpush :msg_nuget_push do |nuget|
  nuget.command = "#{COMMANDS[:nuget]}"
  nuget.package = "#{File.join(FOLDERS[:nuget], PROJECTS[:msg][:nuget_key] + "." + BUILD_VERSION + '.nupkg')}"
  nuget.create_only = false
end

task :default  => ["env:release", "msbuild", "output", "nugets"]

def get_proj_exe symbol
  File.join 'src', PROJECTS[symbol][:dir], 'bin', CONFIGURATION, "#{PROJECTS[symbol][:title]}.exe"
end

desc "run the sample"
task :sample => ["env:release", :msbuild] do
  ravens = Dir.glob "src/packages/Raven*"
  raven = File.join ravens.sort().first, "server", "Raven.Server.exe"
  domain_service = get_proj_exe :domain_svc
  wpf_client = get_proj_exe :wpf
  sh "start #{raven}"
  sh "start #{domain_service}"
  sh "start #{wpf_client}"
end