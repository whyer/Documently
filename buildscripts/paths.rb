root_folder = File.expand_path("#{File.dirname(__FILE__)}/..")
require "buildscripts/project_details"

# The folders array denoting where to place build artifacts

folders = {
  :root => root_folder,
  :src => "src",
  :build => "build",
  :binaries => "placeholder - environment.rb sets this depending on target",
  :tools => "tools",
  :tests => "build/tests",
  :nuget => "build/nuget",
  :nuspec => "build/nuspec"
}

FOLDERS = folders.merge({

  :app => {
      :test_dir => '',
      :nuspec => "#{File.join(folders[:nuspec], PROJECTS[:app][:nuget_key])}",
      :out => 'placeholder - environment.rb will sets this',
      :test_out => 'placeholder - environment.rb sets this'
  },
  
  :cmds => {
      :test_dir => '',
      :nuspec => "#{File.join(folders[:nuspec], PROJECTS[:cmds][:nuget_key])}",
      :out => 'placeholder - environment.rb will sets this',
      :test_out => 'placeholder - environment.rb sets this'
  },
  
  :domain => {
      :test_dir => 'Documently.Domain.Tests',
      :nuspec => "#{File.join(folders[:nuspec], PROJECTS[:domain][:nuget_key])}",
      :out => 'placeholder - environment.rb will sets this',
      :test_out => 'placeholder - environment.rb sets this'
  },
  
  :domain_svc => {
      :test_dir => '',
      :nuspec => "#{File.join(folders[:nuspec], PROJECTS[:domain_svc][:nuget_key])}",
      :out => 'placeholder - environment.rb will sets this',
      :test_out => 'placeholder - environment.rb sets this'
  },
  
  :index => {
      :test_dir => '',
      :nuspec => "#{File.join(folders[:nuspec], PROJECTS[:index][:nuget_key])}",
      :out => 'placeholder - environment.rb will sets this',
      :test_out => 'placeholder - environment.rb sets this'
  },
  
  :indexer_tests => {
      :test_dir => '',
      :nuspec => "#{File.join(folders[:nuspec], PROJECTS[:indexer_tests][:nuget_key])}",
      :out => 'placeholder - environment.rb will sets this',
      :test_out => 'placeholder - environment.rb sets this'
  },
  
  :infr => {
      :test_dir => '',
      :nuspec => "#{File.join(folders[:nuspec], PROJECTS[:infr][:nuget_key])}",
      :out => 'placeholder - environment.rb will sets this',
      :test_out => 'placeholder - environment.rb sets this'
  },
  
  :rm => {
      :test_dir => '',
      :nuspec => "#{File.join(folders[:nuspec], PROJECTS[:rm][:nuget_key])}",
      :out => 'placeholder - environment.rb will sets this',
      :test_out => 'placeholder - environment.rb sets this'
  },
  
  :specs => {
      :test_dir => '',
      :nuspec => "#{File.join(folders[:nuspec], PROJECTS[:specs][:nuget_key])}",
      :out => 'placeholder - environment.rb will sets this',
      :test_out => 'placeholder - environment.rb sets this'
  },
  
  :wpf => {
      :test_dir => '',
      :nuspec => "#{File.join(folders[:nuspec], PROJECTS[:wpf][:nuget_key])}",
      :out => 'placeholder - environment.rb will sets this',
      :test_out => 'placeholder - environment.rb sets this'
  },
  
  :evtlist => {
      :test_dir => '',
      :nuspec => "#{File.join(folders[:nuspec], PROJECTS[:evtlist][:nuget_key])}",
      :out => 'placeholder - environment.rb will sets this',
      :test_out => 'placeholder - environment.rb sets this'
  },
  
})

FILES = {
  :sln => "src/Documently-CQRS.sln",
  
  :app => {
    :nuspec => File.join(FOLDERS[:app][:nuspec], "#{PROJECTS[:app][:nuget_key]}.nuspec")
  },
  
  :cmds => {
    :nuspec => File.join(FOLDERS[:cmds][:nuspec], "#{PROJECTS[:cmds][:nuget_key]}.nuspec")
  },
  
  :domain => {
    :nuspec => File.join(FOLDERS[:domain][:nuspec], "#{PROJECTS[:domain][:nuget_key]}.nuspec")
  },
  
  :domain_svc => {
    :nuspec => File.join(FOLDERS[:domain_svc][:nuspec], "#{PROJECTS[:domain_svc][:nuget_key]}.nuspec")
  },
  
  :index => {
    :nuspec => File.join(FOLDERS[:index][:nuspec], "#{PROJECTS[:index][:nuget_key]}.nuspec")
  },
  
  :indexer_tests => {
    :nuspec => File.join(FOLDERS[:indexer_tests][:nuspec], "#{PROJECTS[:indexer_tests][:nuget_key]}.nuspec")
  },
  
  :infr => {
    :nuspec => File.join(FOLDERS[:infr][:nuspec], "#{PROJECTS[:infr][:nuget_key]}.nuspec")
  },
  
  :rm => {
    :nuspec => File.join(FOLDERS[:rm][:nuspec], "#{PROJECTS[:rm][:nuget_key]}.nuspec")
  },
  
  :specs => {
    :nuspec => File.join(FOLDERS[:specs][:nuspec], "#{PROJECTS[:specs][:nuget_key]}.nuspec")
  },
  
  :wpf => {
    :nuspec => File.join(FOLDERS[:wpf][:nuspec], "#{PROJECTS[:wpf][:nuget_key]}.nuspec")
  },
  
  :evtlist => {
    :nuspec => File.join(FOLDERS[:evtlist][:nuspec], "#{PROJECTS[:evtlist][:nuget_key]}.nuspec")
  },
  
}

COMMANDS = {
  :nuget => File.join(FOLDERS[:tools], "NuGet.exe"),
  :ilmerge => File.join(FOLDERS[:tools], "ILMerge.exe")
  # nunit etc
}