git submodule add https://github.com/AyeshPerera25/MyProgressTrackerDependanciesLib.git 
git submodule update --init --recursive

https://github.com/AyeshPerera25/MyProgressTrackerDependanciesLib

https://github.com/AyeshPerera25/MyProgressTrackerDependanciesLib.git


git rm --cached "Z:\My Doc\Study\IIT\ Enterprise Application Development\Assignment\CW1\MyProgressTrackerInquiryService\MyProgressTrackerDependanciesLib"


rm -rf "Z:\My Doc\Study\IIT\ Enterprise Application Development\Assignment\CW1\MyProgressTrackerInquiryService\MyProgressTrackerDependanciesLib"


git config --remove-section modules\MyProgressTrackerDependanciesLib


git add .gitmodules
git commit -m "Removed submodule MyProgressTrackerDependanciesLib"


git submodule update --remote --merge
