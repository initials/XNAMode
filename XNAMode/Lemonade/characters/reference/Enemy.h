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


@interface Enemy : FlxManagedSprite
{
	CGFloat timer;
    int originalHealth;
    int index;
    float limitX;
    float limitY;
    
    BOOL isTalking;
    BOOL canRun;
    
    float originalVelocity;
    
    //each character has a speech bubble
    
    //FlxSprite * speechBubble;
    
}
+ (id) enemyWithOrigin:(CGPoint)Origin index:(int)Index;
- (id) initWithOrigin:(CGPoint)Origin   index:(int)Index;

- (NSString *) getCurAnim;
- (void) talk:(BOOL)startTalking;
- (void) run;
- (void) runAndJump;
- (void) runAndJump:(float)jumpHeight;
- (void) runJumpAndTurn:(float)jumpHeight;
- (void) killAddStat;


@property BOOL canRun;
@property CGFloat timer;
@property int originalHealth;
@property int index;
@property float limitX;
@property float limitY;
@property BOOL isTalking;
@property float originalVelocity;
@property float gravityRemovalTimer;
@property float gravityRemovalMaxTimeAllowed;
@property CGPoint lastPosition;
@property float lastVelocity;
@property float lastX;


@end
