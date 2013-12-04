//  Copyright Initials 2011. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
// www.initialscommand.com


@interface Player : FlxManagedSprite
{
    CGFloat jump;
    CGFloat jumpLimit;
    BOOL ableToStartJump;
    BOOL isPlayerControlled;
    BOOL isMale;
    BOOL isAirDashing;
    BOOL dontDash;

    BOOL dying;
    BOOL readyToSwitchCharacters;
    
    float jumpInitialMultiplier;
    float jumpInitialTime;
    float jumpSecondaryMultiplier;
    float jumpTimer;
    
    
    
    int jumpCounter;
    float airDashTimer;
    float moveSpeed;
    float timeOnLeftArrow;
    float timeOnRightArrow;
    BOOL canDoubleJump;
    BOOL _canJump;
    BOOL hasDoubleJumped;
    BOOL isPiggyBacking;
    BOOL cutSceneMode;

    
    
    int startsFirst;
    NSString * levelName;
    int andreInitialFlip;
    int liselotInitialFlip;
    
    int followWidth;
    int followHeight;
    
    float levelStartX;
    float levelStartY;
    

}

+ (id) playerWithOrigin:(CGPoint)Origin;
- (id) initWithOrigin:(CGPoint)Origin;
- (void) stopDash;


@property CGFloat jump;
@property CGFloat jumpLimit;
@property BOOL ableToStartJump;
@property BOOL isPlayerControlled;
@property BOOL isMale;
@property    BOOL isAirDashing;
@property    BOOL dontDash;

@property     BOOL dying;
@property     BOOL readyToSwitchCharacters;
@property float jumpInitialMultiplier;
@property float jumpInitialTime;
@property float jumpSecondaryMultiplier;
@property float jumpTimer;
@property int startsFirst;
@property (assign) NSString * levelName;
@property int andreInitialFlip;
@property int liselotInitialFlip;

@property BOOL cutSceneMode;

@property int jumpCounter;
@property float airDashTimer;
@property float moveSpeed;
@property float timeOnLeftArrow;
@property float timeOnRightArrow;
@property BOOL canDoubleJump;
@property BOOL _canJump;
@property BOOL hasDoubleJumped;

@property BOOL isPiggyBacking;

@property int followWidth;
@property int followHeight;

@property float levelStartX;
@property float levelStartY;




@end
