require "buildscripts/paths"
require "buildscripts/project_details"
require 'semver'

namespace :env do

  task :common do
    # version management
    fv = version SemVer.find.to_s
    revision = (!fv[3] || fv[3] == 0) ? (ENV['BUILD_NUMBER'] || Time.now.strftime('%j%H')) : fv[3] #  (day of year 0-265)(hour 00-24)

    ENV['BUILD_VERSION'] = BUILD_VERSION = "#{ SemVer.new(fv[0], fv[1], fv[2]).format "%M.%m.%p" }.#{revision}"
    puts "Assembly Version: #{BUILD_VERSION}."
    puts "##teamcity[buildNumber '#{BUILD_VERSION}']" # tell teamcity our decision

    # .net/mono configuration management
    ENV['FRAMEWORK'] = FRAMEWORK = ENV['FRAMEWORK'] || (Rake::Win32::windows? ? "net40" : "mono28")
    puts "Framework: #{FRAMEWORK}"
  end

  # configure the output directories
  task :configure, [:str] do |_, args|
    ENV['CONFIGURATION'] = CONFIGURATION = args[:str]
    FOLDERS[:binaries] = File.join(FOLDERS[:build], FRAMEWORK, args[:str].downcase)
    CLEAN.include(File.join(FOLDERS[:binaries], "*"))
  end

  task :set_dirs do


    FOLDERS[:app][:out] = File.join(FOLDERS[:src], PROJECTS[:app][:dir], 'bin', CONFIGURATION)
    CLEAN.include(FOLDERS[:app][:out])

    # for tests
    FOLDERS[:app][:test_out] = File.join(FOLDERS[:src], PROJECTS[:app][:test_dir], 'bin', CONFIGURATION)
    FILES[:app][:test] = File.join(FOLDERS[:app][:test_out], "#{PROJECTS[:app][:test_dir]}.dll")
    CLEAN.include(FOLDERS[:test_out])


    FOLDERS[:cmds][:out] = File.join(FOLDERS[:src], PROJECTS[:cmds][:dir], 'bin', CONFIGURATION)
    CLEAN.include(FOLDERS[:cmds][:out])

    # for tests
    FOLDERS[:cmds][:test_out] = File.join(FOLDERS[:src], PROJECTS[:cmds][:test_dir], 'bin', CONFIGURATION)
    FILES[:cmds][:test] = File.join(FOLDERS[:cmds][:test_out], "#{PROJECTS[:cmds][:test_dir]}.dll")
    CLEAN.include(FOLDERS[:test_out])


    FOLDERS[:domain][:out] = File.join(FOLDERS[:src], PROJECTS[:domain][:dir], 'bin', CONFIGURATION)
    CLEAN.include(FOLDERS[:domain][:out])

    # for tests
    FOLDERS[:domain][:test_out] = File.join(FOLDERS[:src], PROJECTS[:domain][:test_dir], 'bin', CONFIGURATION)
    FILES[:domain][:test] = File.join(FOLDERS[:domain][:test_out], "#{PROJECTS[:domain][:test_dir]}.dll")
    CLEAN.include(FOLDERS[:test_out])


    FOLDERS[:domain_svc][:out] = File.join(FOLDERS[:src], PROJECTS[:domain_svc][:dir], 'bin', CONFIGURATION)
    CLEAN.include(FOLDERS[:domain_svc][:out])

    # for tests
    FOLDERS[:domain_svc][:test_out] = File.join(FOLDERS[:src], PROJECTS[:domain_svc][:test_dir], 'bin', CONFIGURATION)
    FILES[:domain_svc][:test] = File.join(FOLDERS[:domain_svc][:test_out], "#{PROJECTS[:domain_svc][:test_dir]}.dll")
    CLEAN.include(FOLDERS[:test_out])


    FOLDERS[:index][:out] = File.join(FOLDERS[:src], PROJECTS[:index][:dir], 'bin', CONFIGURATION)
    CLEAN.include(FOLDERS[:index][:out])

    # for tests
    FOLDERS[:index][:test_out] = File.join(FOLDERS[:src], PROJECTS[:index][:test_dir], 'bin', CONFIGURATION)
    FILES[:index][:test] = File.join(FOLDERS[:index][:test_out], "#{PROJECTS[:index][:test_dir]}.dll")
    CLEAN.include(FOLDERS[:test_out])


    FOLDERS[:indexer_tests][:out] = File.join(FOLDERS[:src], PROJECTS[:indexer_tests][:dir], 'bin', CONFIGURATION)
    CLEAN.include(FOLDERS[:indexer_tests][:out])

    # for tests
    FOLDERS[:indexer_tests][:test_out] = File.join(FOLDERS[:src], PROJECTS[:indexer_tests][:test_dir], 'bin', CONFIGURATION)
    FILES[:indexer_tests][:test] = File.join(FOLDERS[:indexer_tests][:test_out], "#{PROJECTS[:indexer_tests][:test_dir]}.dll")
    CLEAN.include(FOLDERS[:test_out])


    FOLDERS[:infr][:out] = File.join(FOLDERS[:src], PROJECTS[:infr][:dir], 'bin', CONFIGURATION)
    CLEAN.include(FOLDERS[:infr][:out])

    # for tests
    FOLDERS[:infr][:test_out] = File.join(FOLDERS[:src], PROJECTS[:infr][:test_dir], 'bin', CONFIGURATION)
    FILES[:infr][:test] = File.join(FOLDERS[:infr][:test_out], "#{PROJECTS[:infr][:test_dir]}.dll")
    CLEAN.include(FOLDERS[:test_out])


    FOLDERS[:rm][:out] = File.join(FOLDERS[:src], PROJECTS[:rm][:dir], 'bin', CONFIGURATION)
    CLEAN.include(FOLDERS[:rm][:out])

    # for tests
    FOLDERS[:rm][:test_out] = File.join(FOLDERS[:src], PROJECTS[:rm][:test_dir], 'bin', CONFIGURATION)
    FILES[:rm][:test] = File.join(FOLDERS[:rm][:test_out], "#{PROJECTS[:rm][:test_dir]}.dll")
    CLEAN.include(FOLDERS[:test_out])


    FOLDERS[:specs][:out] = File.join(FOLDERS[:src], PROJECTS[:specs][:dir], 'bin', CONFIGURATION)
    CLEAN.include(FOLDERS[:specs][:out])

    # for tests
    FOLDERS[:specs][:test_out] = File.join(FOLDERS[:src], PROJECTS[:specs][:test_dir], 'bin', CONFIGURATION)
    FILES[:specs][:test] = File.join(FOLDERS[:specs][:test_out], "#{PROJECTS[:specs][:test_dir]}.dll")
    CLEAN.include(FOLDERS[:test_out])


    FOLDERS[:wpf][:out] = File.join(FOLDERS[:src], PROJECTS[:wpf][:dir], 'bin', CONFIGURATION)
    CLEAN.include(FOLDERS[:wpf][:out])

    # for tests
    FOLDERS[:wpf][:test_out] = File.join(FOLDERS[:src], PROJECTS[:wpf][:test_dir], 'bin', CONFIGURATION)
    FILES[:wpf][:test] = File.join(FOLDERS[:wpf][:test_out], "#{PROJECTS[:wpf][:test_dir]}.dll")
    CLEAN.include(FOLDERS[:test_out])


    FOLDERS[:evtlist][:out] = File.join(FOLDERS[:src], PROJECTS[:evtlist][:dir], 'bin', CONFIGURATION)
    CLEAN.include(FOLDERS[:evtlist][:out])

    # for tests
    FOLDERS[:evtlist][:test_out] = File.join(FOLDERS[:src], PROJECTS[:evtlist][:test_dir], 'bin', CONFIGURATION)
    FILES[:evtlist][:test] = File.join(FOLDERS[:evtlist][:test_out], "#{PROJECTS[:evtlist][:test_dir]}.dll")
    CLEAN.include(FOLDERS[:test_out])

  end

  task :dir_tasks do
    all_dirs = []

    [:build, :tools, :tests, :nuget, :nuspec].each do |dir|
      directory FOLDERS[dir]
      all_dirs <<  FOLDERS[dir]
    end

    [:out, :nuspec].each do |dir|
      [:app, :cmds, :domain, :domain_svc, :index, :indexer_tests, :infr, :rm, :specs, :wpf, :evtlist].each{ |k|
        directory FOLDERS[k][dir]
        all_dirs << FOLDERS[k][dir]
      }
    end

    all_dirs.each do |d|
      Rake::Task[d].invoke
    end
  end

  # DEBUG/RELEASE

  desc "set debug environment variables"
  task :debug => [:common] do
    Rake::Task["env:configure"].invoke('Debug')
    Rake::Task["env:set_dirs"].invoke
    Rake::Task["env:dir_tasks"].invoke
  end

  desc "set release environment variables"
  task :release => [:common] do
    Rake::Task["env:configure"].invoke('Release')
    Rake::Task["env:set_dirs"].invoke
    Rake::Task["env:dir_tasks"].invoke
  end

  # FRAMEWORKS

  desc "set net40 framework"
  task :net40 do
    ENV['FRAMEWORK'] = 'net40'
  end

  desc "set net35 framework"
  task :net35 do
    ENV['FRAMEWORK'] = 'net35'
  end

  desc "set net20 framework"
  task :net20 do
    ENV['FRAMEWORK'] = 'net20'
  end

  desc "set mono28 framework"
  task :mono28 do
    ENV['FRAMEWORK'] = 'mono28'
  end

  desc "set mono30 framework"
  task :net30 do
    ENV['FRAMEWORK'] = 'mono30'
  end

  # ENVIRONMENT VARS FOR PRODUCT RELEASES

  desc "set GA envionment variables"
  task :ga do
    puts "##teamcity[progressMessage 'Setting environment variables for GA']"
    ENV['OFFICIAL_RELEASE'] = OFFICIAL_RELEASE = "4000"
  end

  desc "set release candidate environment variables"
  task :rc, [:number] do |t, args|
    puts "##teamcity[progressMessage 'Setting environment variables for Release Candidate']"
    arg_num = args[:number].to_i
    num = arg_num != 0 ? arg_num : 1
    ENV['OFFICIAL_RELEASE'] = OFFICIAL_RELEASE = "#{3000 + num}"
  end

  desc "set beta-environment variables"
  task :beta, [:number] do |t, args|
    puts "##teamcity[progressMessage 'Setting environment variables for Beta']"
    arg_num = args[:number].to_i
    num = arg_num != 0 ? arg_num : 1
    ENV['OFFICIAL_RELEASE'] = OFFICIAL_RELEASE = "#{2000 + num}"
  end

  desc "set alpha environment variables"
  task :alpha, [:number] do |t, args|
    puts "##teamcity[progressMessage 'Setting environment variables for Alpha']"
    arg_num = args[:number].to_i
    num = arg_num != 0 ? arg_num : 1

    ENV['OFFICIAL_RELEASE'] = OFFICIAL_RELEASE = "#{1000 + num}"
  end
end
